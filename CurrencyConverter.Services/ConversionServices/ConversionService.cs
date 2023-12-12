using AutoMapper;
using CurrencyConverter.DataAccessLayer.Entities;
using CurrencyConverter.DataAccessLayer.Repositories;
using CurrencyConverter.HttpClient.OpenExchangeRatesClient;
using CurrencyConverter.Services.DTO;

namespace CurrencyConverter.Services.ConversionServices;

public class ConversionService : IConversionService
{
    private readonly ICurrencyConversionRepository _conversionRepository;
    private readonly IOpenExchangeRatesClient _exchangeRatesClient;
    private readonly IMapper _mapper;

    public ConversionService(ICurrencyConversionRepository conversionRepository,
                             IOpenExchangeRatesClient exchangeRatesClient,
                             IMapper mapper)
    {
        _conversionRepository = conversionRepository;
        _exchangeRatesClient = exchangeRatesClient;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ConversionDto>> GetAllConversionsAsync()
    {
        var conversions = await _conversionRepository.GetAllConversionsAsync();
        return _mapper.Map<IEnumerable<ConversionDto>>(conversions);
    }

    public async Task<ConversionDto> CreateConversionAsync(string fromCurrency, string toCurrency, decimal amount)
    {
        var resultAmount = await _exchangeRatesClient.ConvertAsync(fromCurrency, toCurrency, amount);
        var newConversion = new CurrencyConversion
        {
            FromCurrency = fromCurrency,
            ToCurrency = toCurrency,
            Amount = amount,
            ResultAmount = resultAmount
        };

        var createdConversion = await _conversionRepository.AddConversionAsync(newConversion);
        return _mapper.Map<ConversionDto>(createdConversion);
    }
}