using Models;
using ConverterAPI;

namespace ConverterAPITest;

public class MockDatabaseService : IDatabaseService
{
    public CurrencyConversion[] GetConversions()
    {
        return new CurrencyConversion[]{
            new CurrencyConversion(DateTime.Now, "USD", "EUR", 100, 85.0m),
            new CurrencyConversion(DateTime.Now, "EUR", "USD", 100, 118.0m)
        };
    }

    public void SaveConversion(CurrencyConversion conversion)
    {
    }
}