using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.API.Models;

public class ConversionRequest
{
    [Required]
    [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency code must be a 3-letter uppercase code")]
    public string FromCurrency { get; set; }

    [Required]
    [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency code must be a 3-letter uppercase code")]
    public string ToCurrency { get; set; }

    [Required]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number")]
    public decimal Amount { get; set; }
}
