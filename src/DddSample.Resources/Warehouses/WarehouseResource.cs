using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DddSample.Resources.Warehouses;

public sealed class WarehouseResource
{
  [Required]
  [MaxLength(16)]
  [JsonPropertyName("id")]
  public string? Id { get; init; } // this fields comes from outside, and we are not resposible for initializing it, so it is nullable despite to the fact that it is required

  [Required]
  [JsonPropertyName("address")]
  public WarehouseAddressResource? Address { get; init; }

  [Required]
  [JsonPropertyName("contact")]
  public WarehouseContactResource? Contact { get; init; }

  /// <summary>
  /// This field cannot be set or updated directly through CreateWarehouse (POST warehouse) and UpdaetWarehouse (PUT warehouse/{id}) methods. Use the DeleteWarehouse (DELETE warehouse/{id}) or UndeleteWarehouse (POST warehouse/{id}:undelete) methods.
  /// </summary>
  [JsonPropertyName("deleted")]
  public bool? Deleted { get; init; }
}
