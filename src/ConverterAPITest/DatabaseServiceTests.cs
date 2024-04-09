using ConverterAPI;
using Models;
using MySqlConnector;

namespace ConverterAPITests;

public class DatabaseServiceTests
{
    [Test]
    public void GetConversions_ThrowsMySqlException()
    {
        var databaseService = new DatabaseService();

        Assert.Throws(typeof(MySqlException), () => databaseService.GetConversions());
    }
    
    [Test]
    public void SaveConversion_ThrowsArgumentException()
    {
        var databaseService = new DatabaseService();
        var conversion = new CurrencyConversion();
        
        Assert.Throws(typeof(ArgumentException), () => databaseService.SaveConversion(conversion));
    }
    
    [Test]
    public void SaveConversion_ThrowsArgumentNullException()
    {
        var databaseService = new DatabaseService();
        
        Assert.Throws(typeof(ArgumentNullException), () => databaseService.SaveConversion(null));
    }
    
    [Test]
    public void GetConnection_ThrowsMySqlException()
    {
        var databaseService = new DatabaseService();
        
        Assert.Throws(typeof(MySqlException), () => databaseService.GetConnection());
    }
    
}
