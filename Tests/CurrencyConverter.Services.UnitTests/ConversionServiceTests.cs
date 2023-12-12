using AutoFixture.AutoMoq;
using AutoFixture;
using Moq;
using Xunit;
using CurrencyConverter.DataAccessLayer.Repositories;
using AutoMapper;
using CurrencyConverter.HttpClient.OpenExchangeRatesClient;
using CurrencyConverter.DataAccessLayer.Entities;
using CurrencyConverter.Services.ConversionServices;
using CurrencyConverter.Services.DTO;

namespace CurrencyConverter.Services.UnitTests;

public class ConversionServiceTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());
    private readonly Mock<ICurrencyConversionRepository> _mockRepo;
    private readonly Mock<IOpenExchangeRatesClient> _mockExchangeClient;
    private readonly Mock<IMapper> _mockMapper;

    public ConversionServiceTests()
    {
        _mockRepo = _fixture.Freeze<Mock<ICurrencyConversionRepository>>();
        _mockExchangeClient = _fixture.Freeze<Mock<IOpenExchangeRatesClient>>();
        _mockMapper = _fixture.Freeze<Mock<IMapper>>();
    }

    [Fact]
    public async Task GetAllConversionsAsync_ReturnsAllConversions()
    {
        // Arrange
        var fakeConversions = _fixture.CreateMany<CurrencyConversion>().ToList();
        _mockRepo.Setup(repo => repo.GetAllConversionsAsync()).ReturnsAsync(fakeConversions);

        var service = new ConversionService(_mockRepo.Object, _mockExchangeClient.Object, _mockMapper.Object);

        // Act
        var result = await service.GetAllConversionsAsync();

        // Assert
        _mockRepo.Verify(repo => repo.GetAllConversionsAsync(), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<IEnumerable<ConversionDto>>(fakeConversions), Times.Once);
    }

    [Fact]
    public async Task CreateConversionAsync_CreatesAndReturnsConversion()
    {
        // Arrange
        var fromCurrency = "USD";
        var toCurrency = "EUR";
        var amount = 100m;
        var resultAmount = 85m;

        _mockExchangeClient.Setup(client => client.ConvertAsync(fromCurrency, toCurrency, amount))
                           .ReturnsAsync(resultAmount);

        var newConversion = new CurrencyConversion
        {
            FromCurrency = fromCurrency,
            ToCurrency = toCurrency,
            Amount = amount,
            ResultAmount = resultAmount
        };

        _mockRepo.Setup(repo => repo.AddConversionAsync(It.IsAny<CurrencyConversion>()))
                 .ReturnsAsync(newConversion);

        var service = new ConversionService(_mockRepo.Object, _mockExchangeClient.Object, _mockMapper.Object);

        // Act
        var result = await service.CreateConversionAsync(fromCurrency, toCurrency, amount);

        // Assert
        _mockRepo.Verify(repo => repo.AddConversionAsync(It.IsAny<CurrencyConversion>()), Times.Once);
        _mockExchangeClient.Verify(client => client.ConvertAsync(fromCurrency, toCurrency, amount), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<ConversionDto>(newConversion), Times.Once);
    }
}
