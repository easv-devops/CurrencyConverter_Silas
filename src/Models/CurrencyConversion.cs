namespace Models;

public record CurrencyConversion(DateTime Date, string Source, string Target, int Value, decimal Result)
{
}