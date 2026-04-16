using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

internal sealed class PhoneJsonConverter : JsonConverter<Phone>
{
  public override Phone Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();
    if (string.IsNullOrWhiteSpace(value))
    {
      return Phone.None;
    }
    return Phone.Parse(value);
  }

  public override void Write(Utf8JsonWriter writer, Phone value, JsonSerializerOptions options)
  {
    writer.WriteStringValue(value);
  }
}
