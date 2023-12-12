using CurrencyConverter.Services.DTO;

namespace CurrencyConverter.Services.ConversionServices;

public interface IConversionService
{
    Task<IEnumerable<ConversionDto>> GetAllConversionsAsync();
    Task<ConversionDto> CreateConversionAsync(string fromCurrency, string toCurrency, decimal amount);
}
