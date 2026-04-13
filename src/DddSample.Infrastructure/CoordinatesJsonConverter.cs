using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

public sealed class CoordinatesJsonConverter : JsonConverter<Coordinates>
{
  public override Coordinates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException($"Invalid TokenType {reader.TokenType} to read Coordinates");
    }

    Latitude? latitude = null;
    Longitude? longitude = null;
    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read Coordinates property");
      }

      string? propertyName = reader.GetString();
      reader.Read();
      switch (propertyName)
      {
        case "latitude":
          latitude = JsonSerializer.Deserialize<Latitude>(ref reader, options);
          break;
        case "longitude":
          longitude = JsonSerializer.Deserialize<Longitude>(ref reader, options);
          break;
        default:
          throw new JsonException($"Unknown propertyName {propertyName} to read Coordinates property");
      }
    }

    return new Coordinates
    (
      latitude ?? Latitude.Zero,
      longitude ?? Longitude.Zero
    );
  }

  public override void Write(Utf8JsonWriter writer, Coordinates value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WritePropertyName("latitude");
    JsonSerializer.Serialize(writer, value.Latitude, options);

    writer.WritePropertyName("longitude");
    JsonSerializer.Serialize(writer, value.Longitude, options);

    writer.WriteEndObject();
  }
}
