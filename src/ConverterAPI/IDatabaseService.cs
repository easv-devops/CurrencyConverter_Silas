using Models;

namespace ConverterAPI;

public interface IDatabaseService
{
    CurrencyConversion[] GetConversions();
    void SaveConversion(CurrencyConversion conversion);
}