using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

internal sealed class LongitudeJsonConverter : JsonConverter<Longitude>
{
  public override Longitude Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    double longitudeValue = reader.GetDouble();
    return new Longitude(longitudeValue);
  }

  public override void Write(Utf8JsonWriter writer, Longitude value, JsonSerializerOptions options)
  {
    writer.WriteNumberValue(value);
  }
}
