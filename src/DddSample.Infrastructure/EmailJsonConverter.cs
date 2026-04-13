using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

public sealed class EmailJsonConverter : JsonConverter<Email>
{
  public override Email Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();
    if (string.IsNullOrWhiteSpace(value))
    {
      return Email.None;
    }
    return Email.Parse(value);
  }

  public override void Write(Utf8JsonWriter writer, Email value, JsonSerializerOptions options)
  {
    writer.WriteStringValue(value);
  }
}
