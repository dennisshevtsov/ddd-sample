using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using System.Text.Json;

namespace DddSample.Infrastructure.DeliveryPoints.Test;

[TestClass]
public sealed class DeliveryPointOpeningHoursJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true,
    IndentCharacter = ' ',
    IndentSize = 2,

    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new DeliveryPointOpeningHoursJsonConverter(),
      new TimePeriodJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type DeliveryPointOpeningHours is serialized a snake-case JSON is expected")]
  public void Serialize_DeliveryPointOpeningHours_CorrectJsonReturned()
  {
    // Arrange
    DeliveryPointOpeningHours openingHours = new
    (
      worksOnHolidays: true,
      mon: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
      tue: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
      wed: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
      thu: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
      fri: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
      sat: new TimePeriod(from: new TimeOnly(hour: 10, minute: 00), to: new TimeOnly(hour: 20, minute: 30)),
      sun: new TimePeriod(from: new TimeOnly(hour: 10, minute: 00), to: new TimeOnly(hour: 20, minute: 30))
    );

    // Act
    string actual = JsonSerializer.Serialize(openingHours, _jsonSerializerOptions);

    // Assert
    string expected = @"{
  ""works_on_holidays"": true,
  ""mon"": ""09:00-22:00"",
  ""tue"": ""09:00-22:00"",
  ""wed"": ""09:00-22:00"",
  ""thu"": ""09:00-22:00"",
  ""fri"": ""09:00-22:00"",
  ""sat"": ""10:00-20:30"",
  ""sun"": ""10:00-20:30""
}";
    Assert.AreEqual(expected, actual);
  }
}
