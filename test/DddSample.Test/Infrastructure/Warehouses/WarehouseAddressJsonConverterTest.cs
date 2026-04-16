using DddSample.Domain;
using DddSample.Domain.Warehouses;
using System.Text.Json;

namespace DddSample.Infrastructure.Warehouses.Test;

[TestClass]
public sealed class WarehouseAddressJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true,
    IndentCharacter = ' ',
    IndentSize = 2,

    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new AddressJsonConverter(),
      new CoordinatesJsonConverter(),
      new WarehouseAddressJsonConverter(),
      new LatitudeJsonConverter(),
      new LongitudeJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type WarehouseAddress is serialized, a snake-case JSON is expected")]
  public void Serialize_WarehouseAddress_CorrectJsonReturned()
  {
    // Arrange
    WarehouseAddress address = new
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
    Assert.AreEqual(expected, actual);
  }
}
