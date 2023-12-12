namespace CurrencyConverter.HttpClient.OpenExchangeRatesClient;

public interface IOpenExchangeRatesClient
{
    public Task<decimal> ConvertAsync(string fromCcy, string toCcy, decimal value);
}
