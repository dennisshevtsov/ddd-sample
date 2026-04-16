using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Infrastructure;
using DddSample.Infrastructure.DeliveryPoints;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DddSample.Test.Infrastructure.DeliveryPoints;

[TestClass]
public sealed class DeliveryPointAddressJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new AddressJsonConverter(),
      new CoordinatesJsonConverter(),
      new DeliveryPointAddressJsonConverter(),
      new LatitudeJsonConverter(),
      new LongitudeJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type DeliveryPointAddress is serialized a snake-case JSON is expected")]
  public void Serialize_DeliveryPointAddress_CorrectJsonReturned()
  {
    // Arrange
    DeliveryPointAddress address = new
    (
      address: Address.Parse("test address"),
      coordinates: new Coordinates
      (
        latitude: new Latitude(1D),
        longitude: new Longitude(2D)
      )
    );

    // Act
    string actual = JsonSerializer.Serialize(address, _jsonSerializerOptions);

    // Assert
    string expected = @"{
      ""address"": ""test address"",
      ""coordinates"": {
        ""latitude"": 1,
        ""longitude"": 2
      }
    }";
    Assert.IsTrue(expected.JsonEquals(actual));
  }
}
