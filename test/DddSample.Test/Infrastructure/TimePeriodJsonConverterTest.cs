using DddSample.Domain;
using System.Text.Json;

namespace DddSample.Infrastructure.Test;

[TestClass]
public sealed class TimePeriodJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true,
    IndentCharacter = ' ',
    IndentSize = 2,

    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new TimePeriodJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type TimePeriod is serialized a \"HH:mm-HH:mm\" string is expected")]
  public void Serialize_TimePeriod_CorrectJsonReturned()
  {
    // Arrange
    TimePeriod timePeriod = new
    (
      from: new TimeOnly(hour: 14, minute: 57),
      to: new TimeOnly(hour: 23, minute: 17)
    );

    // Act
    string actual = JsonSerializer.Serialize(timePeriod, _jsonSerializerOptions);

    // Assert
    string expected = "\"14:57-23:17\"";
    Assert.AreEqual(expected, actual);
  }

  [TestMethod(DisplayName = "When a JSON of an object of type TimePeriod is serialized an object of type TimePeriod is expected")]
  public void Deserialize_Json_CorrectObjectReturned()
  {
    // Arrange
    string value = "\"14:57-23:17\"";

    // Act
    TimePeriod? actual = JsonSerializer.Deserialize<TimePeriod>(value, _jsonSerializerOptions);

    // Assert
    TimePeriod expected = new
    (
      from: new TimeOnly(hour: 14, minute: 57),
      to: new TimeOnly(hour: 23, minute: 17)
    );
    Assert.AreEqual(expected, actual);
  }
}
