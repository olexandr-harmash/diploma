using System.Text.Json;

namespace diploma.Shared.Exceptions;

public class ExceptionDetails(string message, int statusCode)
{
    public int StatusCode { get; set; } = statusCode;

    public string? Message { get; set; } = message;

    public override string ToString() => JsonSerializer.Serialize(this);
}

