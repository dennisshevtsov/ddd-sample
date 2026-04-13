using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure.DeliveryPoints;

internal sealed class DeliveryPointAddressJsonConverter : JsonConverter<DeliveryPointAddress>
{
  public override DeliveryPointAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(DeliveryPointAddress)}");
    }

    Address? address = null;
    Coordinates? coordinates = null;

    string addressPropertyName = options.ConvertName(nameof(DeliveryPointAddress.Address));
    string coordinatesPropertyName = options.ConvertName(nameof(DeliveryPointAddress.Coodinates));

    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(DeliveryPointAddress)} property");
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

      throw new JsonException($"Unknown propertyName {propertyName} to read {nameof(DeliveryPointAddress)} property");
    }

    return new DeliveryPointAddress
    (
      address ?? Address.None,
      coordinates ?? Coordinates.None
    );
  }

  public override void Write(Utf8JsonWriter writer, DeliveryPointAddress value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointAddress.Address)));
    JsonSerializer.Serialize(writer, value.Address, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointAddress.Coodinates)));
    JsonSerializer.Serialize(writer, value.Coodinates, options);

    writer.WriteEndObject();
  }
}
