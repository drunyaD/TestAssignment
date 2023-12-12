using CurrencyConverter.DataAccessLayer.EF;
using CurrencyConverter.DataAccessLayer.Repositories;
using CurrencyConverter.HttpClient.OpenExchangeRatesClient;
using CurrencyConverter.Services.AutoMapper;
using CurrencyConverter.Services.ConversionServices;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CurrencyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICurrencyConversionRepository, CurrencyConversionRepository>();

            builder.Services.AddSingleton<IOpenExchangeRatesClient>(new OpenExchangeRatesClient(
                builder.Configuration["OpenExchangeRates:BaseUrl"],
                builder.Configuration["OpenExchangeRates:ApiKey"]
                ));

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddScoped<IConversionService, ConversionService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}