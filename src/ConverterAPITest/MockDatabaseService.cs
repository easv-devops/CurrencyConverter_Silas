using Models;
using ConverterAPI;

namespace ConverterAPITest;

public class MockDatabaseService : IDatabaseService
{
    public CurrencyConversion[] GetConversions()
    {
        return new CurrencyConversion[]{
            new (DateTime.Now, "USD", "EUR", 100, 88.5m),
            new (DateTime.Now, "EUR", "USD", 100, 112.9m),
            new (DateTime.Now, "USD", "JPY", 100, 11000m)
        };
    }

    public void SaveConversion(CurrencyConversion conversion)
    {
    }
}