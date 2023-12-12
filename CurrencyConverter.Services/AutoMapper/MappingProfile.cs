using AutoMapper;
using CurrencyConverter.DataAccessLayer.Entities;
using CurrencyConverter.Services.DTO;

namespace CurrencyConverter.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CurrencyConversion, ConversionDto>();
    }
}
