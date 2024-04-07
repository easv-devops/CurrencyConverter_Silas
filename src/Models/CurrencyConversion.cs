namespace Models
{
    public class CurrencyConversion
    {
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public int Value { get; set; }
        public double Result { get; set; }

        // Parameterless constructor
        public CurrencyConversion() { }

        // Constructor that matches the SQL query
        public CurrencyConversion(DateTime date, string source, string target, int value, double result)
        {
            Date = date;
            Source = source;
            Target = target;
            Value = value;
            Result = result;
        }
    }
}