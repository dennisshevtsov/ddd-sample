using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources.DeliveryPoints;

public sealed class DeliveryPointResource
{
  [MinLength(36)]
  [MaxLength(36)]
  [JsonPropertyName("id")]
  public string? Id { get; init; }

  [Required]
  [MinLength(36)]
  [MaxLength(36)]
  [JsonPropertyName("warehouseId")]
  public string? WarehouseId { get; init; }

  [Required]
  [JsonPropertyName("address")]
  public DeliveryPointAddressResource? Address { get; init; }

  [Required]
  [JsonPropertyName("openingHours")]
  public DeliveryPointOpeningHoursResource? OpeningHours { get; init; }
}
