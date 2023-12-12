using CurrencyConverter.API.Models;
using CurrencyConverter.Services.ConversionServices;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConversionsController : ControllerBase
    {
        private readonly IConversionService _conversionService;

        public CurrencyConversionsController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conversions = await _conversionService.GetAllConversionsAsync();
            return Ok(conversions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConversionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _conversionService.CreateConversionAsync(
                request.FromCurrency,
                request.ToCurrency,
                request.Amount);

            return Created(string.Empty, result);
        }
    }
}
