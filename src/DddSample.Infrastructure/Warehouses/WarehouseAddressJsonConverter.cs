using DddSample.Domain;
using DddSample.Domain.Warehouses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure.Warehouses;

internal sealed class WarehouseAddressJsonConverter : JsonConverter<WarehouseAddress>
{
  public override WarehouseAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(WarehouseAddress)}");
    }

    Address? address = null;
    Coordinates? coordinates = null;

    string addressPropertyName = ConvertName(nameof(WarehouseAddress.Address), options);
    string coordinatesPropertyName = ConvertName(nameof(WarehouseAddress.Coodinates), options);

    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(WarehouseAddress)} property");
      }

      string? propertyName = reader.GetString();
      reader.Read();

      if (propertyName == addressPropertyName)
      {
        address = JsonSerializer.Deserialize<Address>(ref reader, options);
        continue;
      }

      if (propertyName == coordinatesPropertyName)
      {
        coordinates = JsonSerializer.Deserialize<Coordinates>(ref reader, options);
        continue;
      }

      throw new JsonException($"Unknown propertyName {propertyName} to read {nameof(WarehouseAddress)} property");
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

    writer.WritePropertyName(ConvertName(nameof(WarehouseAddress.Address), options));
    JsonSerializer.Serialize(writer, value.Address, options);

    writer.WritePropertyName(ConvertName(nameof(WarehouseAddress.Coodinates), options));
    JsonSerializer.Serialize(writer, value.Coodinates, options);

    writer.WriteEndObject();
  }

  private static string ConvertName(string name, JsonSerializerOptions options)
  {
    if (options.PropertyNamingPolicy is null)
    {
      return name;
    }
    return options.PropertyNamingPolicy.ConvertName(name);
  }
}
