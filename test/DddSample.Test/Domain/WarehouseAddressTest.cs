using DddSample.Domain;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure;
using DddSample.Infrastructure.Warehouses;
using System.Text.Json;

namespace DddSample.Test.Domain;

[TestClass]
public sealed class WarehouseAddressTest
{
  [TestMethod]
  public void Serialize_WarehouseAddress_Serialized()
  {
    // Arrange
    WarehouseAddress address = new
    (
      address: Address.Parse("test address"),
      coodinates: new Coordinates
      (
        latitude: new Latitude(1D),
        longitude: new Longitude(2D)
      )
    );

    JsonSerializerOptions options = new()
    {
      Converters =
      {
        new AddressJsonConverter(),
        new LatitudeJsonConverter(),
        new LongitudeJsonConverter(),
        new WarehouseAddressJsonConverter(),
      },
    };

    // Act
    string warehouseJson = JsonSerializer.Serialize(address, options);

    // Assert
    Assert.IsNotEmpty(warehouseJson);
  }
}
