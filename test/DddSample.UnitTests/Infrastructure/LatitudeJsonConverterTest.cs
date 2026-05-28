using DddSample.Domain;
using System.Text.Json;

namespace DddSample.Infrastructure.UnitTest;

public sealed class LatitudeJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new LatitudeJsonConverter(),
    },
  };

  [Fact(DisplayName = "When an object of type Latitude is serialized, a number is expected")]
  public void Serialize_Latitude_CorrectJsonReturned()
  {
    // Arrange
    Latitude latitude = new(12.34D);

    // Act
    string actual = JsonSerializer.Serialize(latitude, _jsonSerializerOptions);

    // Assert
    string expected = "12.34";
    Assert.Equal(expected, actual);
  }
}
