using System.Text.Json.Serialization;

namespace CurrencyConverter.HttpClient.Models;

public class ConvertResponse
{
    [JsonPropertyName("response")]
    public decimal ResultAmount { get; set; }
}
