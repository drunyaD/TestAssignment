using CurrencyConverter.DataAccessLayer.EF;
using CurrencyConverter.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.DataAccessLayer.Repositories;

public class CurrencyConversionRepository : ICurrencyConversionRepository
{
    private readonly CurrencyDbContext _context;

    public CurrencyConversionRepository(CurrencyDbContext context)
    {
        _context = context;
    }

    public async Task<CurrencyConversion> AddConversionAsync(CurrencyConversion conversion)
    {
        _context.CurrencyConversions.Add(conversion);
        await _context.SaveChangesAsync();
        return conversion;
    }

    public async Task<List<CurrencyConversion>> GetAllConversionsAsync()
    {
        return await _context.CurrencyConversions.ToListAsync();
    }
}
