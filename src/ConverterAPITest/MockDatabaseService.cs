using Models;
using ConverterAPI;

namespace ConverterAPITest;

public class MockDatabaseService : IDatabaseService
{
    public CurrencyConversion[] GetConversions()
    {
        return new CurrencyConversion[]{
            new (DateTime.Now, "USD", "EUR", 100, 88.5),
            new (DateTime.Now, "EUR", "USD", 100, 112.9),
            new (DateTime.Now, "USD", "JPY", 100, 11000)
        };
    }

    public void SaveConversion(CurrencyConversion conversion)
    {
    }
}