using CurrencyConverter.DataAccessLayer.Entities;

namespace CurrencyConverter.DataAccessLayer.Repositories;

public interface ICurrencyConversionRepository
{
    Task<CurrencyConversion> AddConversionAsync(CurrencyConversion conversion);

    Task<List<CurrencyConversion>> GetAllConversionsAsync();
}
