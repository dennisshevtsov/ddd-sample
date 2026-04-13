using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

internal sealed class LatitudeJsonConverter : JsonConverter<Latitude>
{
  public override Latitude Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    double latitudeValue = reader.GetDouble();
    return new Latitude(latitudeValue);
  }

  public override void Write(Utf8JsonWriter writer, Latitude value, JsonSerializerOptions options)
  {
    writer.WriteNumberValue(value);
  }
}
