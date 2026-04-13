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

    Email[]? emails = null;
    Phone[]? phones = null;

    string emailsPropertyName = options.ConvertName(nameof(WarehouseContact.Emails));
    string phonesPropertyName = options.ConvertName(nameof(WarehouseContact.Phones));

    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(WarehouseContact)} property");
      }

      string? propertyName = reader.GetString();
      reader.Read();

      if (propertyName == emailsPropertyName)
      {
        emails = JsonSerializer.Deserialize<Email[]>(ref reader, options);
        continue;
      }

      if (propertyName == phonesPropertyName)
      {
        phones = JsonSerializer.Deserialize<Phone[]>(ref reader, options);
        continue;
      }

      throw new JsonException($"Unknown propertyName {propertyName} to read {nameof(WarehouseContact)} property");
    }

    return new WarehouseContact(emails ?? [], phones ?? []);
  }

  public override void Write(Utf8JsonWriter writer, WarehouseContact value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WritePropertyName(options.ConvertName(nameof(WarehouseContact.Emails)));
    JsonSerializer.Serialize(writer, value.Emails, options);

    writer.WritePropertyName(options.ConvertName(nameof(WarehouseContact.Phones)));
    JsonSerializer.Serialize(writer, value.Phones, options);

    writer.WriteEndObject();
  }
}
