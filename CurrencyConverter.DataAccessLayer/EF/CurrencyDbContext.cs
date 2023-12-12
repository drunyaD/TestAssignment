using CurrencyConverter.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.DataAccessLayer.EF;

public class CurrencyDbContext : DbContext
{
    public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
        : base(options)
    {
    }

    public DbSet<CurrencyConversion> CurrencyConversions { get; set; }
}