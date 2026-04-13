using DddSample.Domain;
using DddSample.Domain.Warehouses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure.Warehouses;

public sealed class WarehouseContactJsonConverter : JsonConverter<WarehouseContact>
{
  public override WarehouseContact? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException($"Invalid TokenType {reader.TokenType} to read WarehouseContact");
    }

    Phone[]? phones = null;
    Email[]? emails = null;
    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read WarehouseContact property");
      }

      string? propertyName = reader.GetString();
      reader.Read();
      switch (propertyName)
      {
        case "emails":
          emails = JsonSerializer.Deserialize<Email[]>(ref reader, options);
          break;
        case "phones":
          phones = JsonSerializer.Deserialize<Phone[]>(ref reader, options);
          break;
        default:
          throw new JsonException($"Unknown propertyName {propertyName} to read WarehouseContact property");
      }
    }

    return new WarehouseContact(phones ?? [], emails ?? []);
  }

  public override void Write(Utf8JsonWriter writer, WarehouseContact value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WritePropertyName("emails");
    JsonSerializer.Serialize(writer, value.Emails, options);

    writer.WritePropertyName("phones");
    JsonSerializer.Serialize(writer, value.Phones, options);

    writer.WriteEndObject();
  }
}
