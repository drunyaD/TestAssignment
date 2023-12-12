namespace CurrencyConverter.Services.DTO;

public class ConversionDto
{
    public int Id { get; set; }

    public string FromCurrency { get; set; }

    public string ToCurrency { get; set; }

    public decimal Amount { get; set; }

    public decimal ResultAmount { get; set; }
}
