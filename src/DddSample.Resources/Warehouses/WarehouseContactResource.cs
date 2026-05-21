using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources.Warehouses;

public sealed class WarehouseContactResource
{
  [Required]
  [JsonPropertyName("emails")]
  public IReadOnlyList<string>? Emails { get; init; }

  [Required]
  [JsonPropertyName("phones")]
  public IReadOnlyList<string>? Phones { get; init; }
}
