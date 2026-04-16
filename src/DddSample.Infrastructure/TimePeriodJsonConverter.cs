using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

internal sealed class TimePeriodJsonConverter : JsonConverter<TimePeriod>
{
  public override TimePeriod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();
    if (string.IsNullOrWhiteSpace(value))
    {
      return TimePeriod.None;
    }
    return TimePeriod.Parse(value);
  }

  public override void Write(Utf8JsonWriter writer, TimePeriod value, JsonSerializerOptions options)
  {
    writer.WriteStringValue(value.ToString());
  }
}
