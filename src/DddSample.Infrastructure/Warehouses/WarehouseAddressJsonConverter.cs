using DddSample.Domain;
using DddSample.Domain.Warehouses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure.Warehouses;

public sealed class WarehouseAddressJsonConverter : JsonConverter<WarehouseAddress>
{
  public override WarehouseAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException($"Invalid TokenType {reader.TokenType} to read WarehouseAddress");
    }

    Address? address = null;
    Coordinates? coordinates = null;
    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read WarehouseAddress property");
      }

      string? propertyName = reader.GetString();
      reader.Read();
      switch (propertyName)
      {
        case "address":
          address = JsonSerializer.Deserialize<Address>(ref reader, options);
          break;
        case "coordinates":
          coordinates = JsonSerializer.Deserialize<Coordinates>(ref reader, options);
          break;
        default:
          throw new JsonException($"Unknown propertyName {propertyName} to read WarehouseAddress property");
      }
    }

    return new WarehouseAddress
    (
      address ?? Address.None,
      coordinates ?? Coordinates.None
    );
  }

  public override void Write(Utf8JsonWriter writer, WarehouseAddress value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WritePropertyName("address");
    JsonSerializer.Serialize(writer, value.Address, options);

    writer.WritePropertyName("coordinates");
    JsonSerializer.Serialize(writer, value.Coodinates, options);

    writer.WriteEndObject();
  }
}
