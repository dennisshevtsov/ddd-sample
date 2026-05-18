using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources.Merchants;

public sealed class MerchantResource
{
  [Required]
  [MinLength(36)]
  [MaxLength(36)]
  [JsonPropertyName("id")]
  public string? Id { get; init; }

  [Required]
  [MinLength(2)]
  [MaxLength(16)]
  [JsonPropertyName("id")]
  public string? Name { get; init; }

  /// <summary>
  /// This field cannot be set or updated directly through CreateMerchant (POST merchant) and UpdaetMerchant (PUT merchant/{id}) methods. Use the DeleteMerchant (DELETE merchant/{id}) or UndeleteMerchant (POST merchant/{id}:undelete) methods.
  /// </summary>
  [JsonPropertyName("deleted")]
  public bool? Deleted { get; init; }
}
