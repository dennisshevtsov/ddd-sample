using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources;

public sealed class CoordinatesResource
{
  [Required]
  [JsonPropertyName("latitude")]
  public double? Latitude { get; init; }

  [Required]
  [JsonPropertyName("longitude")]
  public double? Longitude { get; init; }
}
