using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

internal sealed class CoordinatesJsonConverter : JsonConverter<Coordinates>
{
  public override Coordinates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(Coordinates)}");
    }

    Latitude? latitude = null;
    Longitude? longitude = null;

    string latitudePropertyName = ConvertName(nameof(Coordinates.Latitude), options);
    string longitudePropertyName = ConvertName(nameof(Coordinates.Longitude), options);

    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(Coordinates)} property");
      }

      string ? propertyName = reader.GetString();
      reader.Read();

      if (propertyName == latitudePropertyName)
      {
        latitude = JsonSerializer.Deserialize<Latitude>(ref reader, options);
        continue;
      }

      if (propertyName == longitudePropertyName)
      {
        longitude = JsonSerializer.Deserialize<Longitude>(ref reader, options);
        continue;
      }

      throw new JsonException($"Unknown propertyName {propertyName} to read {nameof(Coordinates)} property");
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

    writer.WritePropertyName(ConvertName(nameof(Coordinates.Latitude), options));
    JsonSerializer.Serialize(writer, value.Latitude, options);

    writer.WritePropertyName(ConvertName(nameof(Coordinates.Longitude), options));
    JsonSerializer.Serialize(writer, value.Longitude, options);

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
