using CurrencyConverter.HttpClient.Exceptions;
using CurrencyConverter.HttpClient.Models;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace CurrencyConverter.HttpClient.OpenExchangeRatesClient;

public class OpenExchangeRatesClient : IOpenExchangeRatesClient
{
    private readonly RestClient _client;
    private readonly string _apiKey;

    public OpenExchangeRatesClient(string baseUrl, string apiKey)
    {
        _client = new RestClient(baseUrl);
        _apiKey = apiKey;
    }

    public async Task<decimal> ConvertAsync(string fromCcy, string toCcy, decimal value)
    {
        var request = new RestRequest($"/convert/{value}/{fromCcy}/{toCcy}?app_id={_apiKey}", Method.Get);

        var response = await _client.ExecuteAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var convertResponse = JsonSerializer.Deserialize<ConvertResponse>(response.Content)!;
            return convertResponse.ResultAmount;
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response.Content)!;
            throw new ApiClientException($"API Error: {errorResponse.Message}, Status: {errorResponse.Status}");
        }
        else
        {
            throw new ApiClientException($"Unhandled status code: {response.StatusCode}");
        }
    }
}
