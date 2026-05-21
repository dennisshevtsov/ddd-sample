using System.Text.Json.Serialization;

namespace DddSample.Resources;

public sealed class ErrorMetadata
{
  [JsonPropertyName("code")]
  public required string Code { get; init; }

  [JsonPropertyName("message")]
  public required string Message { get; init; }

  [JsonPropertyName("details")]
  public object? Details { get; init; }
}
