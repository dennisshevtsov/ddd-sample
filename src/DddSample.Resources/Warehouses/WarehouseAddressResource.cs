using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources.Warehouses;

public sealed class WarehouseAddressResource
{
  [Required]
  [MaxLength(128)]
  [JsonPropertyName("address")]
  public string? Address { get; init; }

  [Required]
  [JsonPropertyName("coordinates")]
  public CoordinatesResource? Coordinates { get; init; }

}
