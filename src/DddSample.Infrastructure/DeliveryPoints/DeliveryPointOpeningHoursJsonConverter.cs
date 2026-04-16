using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure.DeliveryPoints;

internal sealed class DeliveryPointOpeningHoursJsonConverter : JsonConverter<DeliveryPointOpeningHours>
{
  public override DeliveryPointOpeningHours? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(DeliveryPointOpeningHours)}");
    }

    bool? worksOnHolidays = null;
    TimePeriod? mon = null;
    TimePeriod? tue = null;
    TimePeriod? wed = null;
    TimePeriod? thu = null;
    TimePeriod? fri = null;
    TimePeriod? sat = null;
    TimePeriod? sun = null;

    string worksOnHolyDaysPropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.WorksOnHolidays));
    string monPropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.Mon));
    string tuePropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.Tue));
    string wedPropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.Wed));
    string thuPropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.Thu));
    string friPropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.Fri));
    string satPropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.Sat));
    string sunPropertyName = options.ConvertName(nameof(DeliveryPointOpeningHours.Sun));

    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    {
      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException($"Invalid TokenType {reader.TokenType} to read {nameof(DeliveryPointOpeningHours)} property");
      }

      string? propertyName = reader.GetString();
      reader.Read();

      if (propertyName == worksOnHolyDaysPropertyName)
      {
        worksOnHolidays = reader.GetBoolean();
        continue;
      }

      if (propertyName == monPropertyName)
      {
        mon = JsonSerializer.Deserialize<TimePeriod>(ref reader, options);
        continue;
      }

      if (propertyName == tuePropertyName)
      {
        tue = JsonSerializer.Deserialize<TimePeriod>(ref reader, options);
        continue;
      }

      if (propertyName == wedPropertyName)
      {
        wed = JsonSerializer.Deserialize<TimePeriod>(ref reader, options);
        continue;
      }

      if (propertyName == thuPropertyName)
      {
        thu = JsonSerializer.Deserialize<TimePeriod>(ref reader, options);
        continue;
      }

      if (propertyName == friPropertyName)
      {
        fri = JsonSerializer.Deserialize<TimePeriod>(ref reader, options);
        continue;
      }

      if (propertyName == satPropertyName)
      {
        sat = JsonSerializer.Deserialize<TimePeriod>(ref reader, options);
        continue;
      }

      if (propertyName == sunPropertyName)
      {
        sun = JsonSerializer.Deserialize<TimePeriod>(ref reader, options);
        continue;
      }

      throw new JsonException($"Unknown propertyName {propertyName} to read {nameof(DeliveryPointOpeningHours)} property");
    }

    return new DeliveryPointOpeningHours
    (
      worksOnHolidays ?? false,
      mon ?? TimePeriod.None,
      tue ?? TimePeriod.None,
      wed ?? TimePeriod.None,
      thu ?? TimePeriod.None,
      fri ?? TimePeriod.None,
      sat ?? TimePeriod.None,
      sun ?? TimePeriod.None
    );
  }

  public override void Write(Utf8JsonWriter writer, DeliveryPointOpeningHours value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.WorksOnHolidays)));
    JsonSerializer.Serialize(writer, value.WorksOnHolidays, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.Mon)));
    JsonSerializer.Serialize(writer, value.Mon, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.Tue)));
    JsonSerializer.Serialize(writer, value.Tue, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.Wed)));
    JsonSerializer.Serialize(writer, value.Wed, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.Thu)));
    JsonSerializer.Serialize(writer, value.Thu, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.Fri)));
    JsonSerializer.Serialize(writer, value.Fri, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.Sat)));
    JsonSerializer.Serialize(writer, value.Sat, options);

    writer.WritePropertyName(options.ConvertName(nameof(DeliveryPointOpeningHours.Sun)));
    JsonSerializer.Serialize(writer, value.Sun, options);

    writer.WriteEndObject();
  }
}
