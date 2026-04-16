using DddSample.Domain;
using System.Text.Json;

namespace DddSample.Infrastructure.Test;

[TestClass]
public sealed class LongitudeJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new LongitudeJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type Longitude is serialized, a number is expected")]
  public void Serialize_Longitude_CorrectJsonReturned()
  {
    // Arrange
    Longitude longitude = new(12.34D);

    // Act
    string actual = JsonSerializer.Serialize(longitude, _jsonSerializerOptions);

    // Assert
    string expected = "12.34";
    Assert.AreEqual(expected, actual);
  }
}
