using DddSample.Domain;
using System.Text.Json;

namespace DddSample.Infrastructure.UnitTest;

public sealed class CoordinatesJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true,
    IndentCharacter = ' ',
    IndentSize = 2,

    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new CoordinatesJsonConverter(),
      new LatitudeJsonConverter(),
      new LongitudeJsonConverter(),
    },
  };

  [Fact(DisplayName = "When an object of type Coordinates is serialized, a snake-case JSON is expected")]
  public void Serialize_Coordinates_CorrectJsonReturned()
  {
    // Arrange
    Coordinates coordinates = new
    (
      latitude: new Latitude(1D),
      longitude: new Longitude(2D)
    );

    // Act
    string actual = JsonSerializer.Serialize(coordinates, _jsonSerializerOptions);

    // Assert
    string expected =
      """
      {
        "latitude": 1,
        "longitude": 2
      }
      """;
    Assert.Equal(expected, actual);
  }
}
