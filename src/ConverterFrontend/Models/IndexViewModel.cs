using Models;

namespace ConverterFrontend.Models;

public class IndexViewModel
{
    public decimal Result { get; set; }
    public CurrencyConversion[] Conversions { get; set; }
}