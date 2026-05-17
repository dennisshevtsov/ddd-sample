using System.Text.Json.Serialization;

namespace DddSample.Resources;

public sealed class ListResponse<TResource> where TResource : class
{
  [JsonPropertyName("results")]
  public required IReadOnlyList<TResource> Results { get; set; }

  [JsonPropertyName("nextPageToken")]
  public string? NextPageToken { get; set; }
}
