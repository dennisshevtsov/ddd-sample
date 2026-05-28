using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources.DeliveryPoints;

public sealed class DeliveryPointOpeningHoursResource
{
  [Required]
  [JsonPropertyName("worksOnHolidays")]
  public bool? WorksOnHolidays { get; init; }

  [Required]
  [JsonPropertyName("mon")]
  public string? Mon { get; init; }

  [Required]
  [JsonPropertyName("tue")]
  public string? Tue { get; init; }

  [Required]
  [JsonPropertyName("wed")]
  public string? Wed { get; init; }

  [Required]
  [JsonPropertyName("thu")]
  public string? Thu { get; init; }

  [Required]
  [JsonPropertyName("fri")]
  public string? Fri { get; init; }

  [Required]
  [JsonPropertyName("sat")]
  public string? Sat { get; init; }

  [Required]
  [JsonPropertyName("sun")]
  public string? Sun { get; init; }
}
