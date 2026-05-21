using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources;

public sealed class CoordinatesResource
{
  [Required]
  [Range(0D, 90D)]
  [JsonPropertyName("latitude")]
  public double? Latitude { get; init; }

  [Required]
  [Range(0D, 180D)]
  [JsonPropertyName("longitude")]
  public double? Longitude { get; init; }
}
