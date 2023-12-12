namespace CurrencyConverter.HttpClient.Exceptions;

public class ApiClientException : Exception
{
    public ApiClientException() : base() { }

    public ApiClientException(string message) : base(message) { }

    public ApiClientException(string message, Exception innerException) : base(message, innerException) { }
}
