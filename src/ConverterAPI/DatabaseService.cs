using System.Diagnostics.CodeAnalysis;
using MySqlConnector;
using Dapper;
using Models;

namespace ConverterAPI;

public class DatabaseService : IDatabaseService
{   
    public CurrencyConversion[] GetConversions()
    {
        using var connection = GetConnection();
        return connection.Query<CurrencyConversion>("SELECT `date`, source, target, `value`, result FROM Conversions ORDER BY `date` DESC").ToArray();
    }

    public void SaveConversion(CurrencyConversion conversion)
    {
        if (conversion == null)
        {
            throw new ArgumentNullException(nameof(conversion));
        }
        
        if (conversion.Value <= 0)
        {
            throw new ArgumentException("Value must be greater than 0", nameof(conversion));
        }
        
        using var connection = GetConnection();
        using var transaction = connection.BeginTransaction();
        
        connection.Execute(@"
            INSERT INTO Conversions (`date`, source, target, `value`, result) VALUES (@Date, @Source, @Target, @Value, @Result)
            ", conversion, transaction);
            
        transaction.Commit();
    }

    public MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection("Server=mariadb;Database=conversions;Uid=myuser;Pwd=mypassword;");
        connection.Open();
        return connection;
    }
}