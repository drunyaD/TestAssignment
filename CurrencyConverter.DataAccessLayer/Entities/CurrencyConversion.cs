namespace CurrencyConverter.DataAccessLayer.Entities
{
    public class CurrencyConversion
    {
        public int Id { get; set; }

        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public decimal Amount { get; set; }

        public decimal ResultAmount { get; set; }

        public DateTime ConversionTime { get; set; } = DateTime.UtcNow;
    }
}
