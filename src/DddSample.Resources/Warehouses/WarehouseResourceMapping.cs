using DddSample.Domain;
using DddSample.Domain.Warehouses;

namespace DddSample.Resources.Warehouses;

internal static class WarehouseResourceMapping
{
  internal static WarehouseResource ToResource(this Warehouse warehouse)
  {
    return new()
    {
      Id = warehouse.Id.ToString(),
      Address = new WarehouseAddressResource
      {
        Address = warehouse.Address.Address,
        Coordinates = new CoordinatesResource
        {
          Latitude = warehouse.Address.Coordinates.Latitude,
          Longitude = warehouse.Address.Coordinates.Longitude,
        },
      },
      Contact = new WarehouseContactResource
      {
        Emails = [.. warehouse.Contact.Emails.Select(x => x.ToString())],
        Phones = [.. warehouse.Contact.Phones.Select(x => x.ToString())],
      },
    };
  }

  internal static Warehouse ToEntity(this WarehouseResource resource, MerchantId merchantId)
  {
    string? address = resource?.Address?.Address;
    ArgumentNullException.ThrowIfNull(address);

    double? latitude = resource?.Address?.Coordinates?.Latitude;
    ArgumentNullException.ThrowIfNull(latitude);

    double? longitude = resource?.Address?.Coordinates?.Longitude;
    ArgumentNullException.ThrowIfNull(longitude);

    IReadOnlyList<string>? emails = resource?.Contact?.Emails;
    ArgumentNullException.ThrowIfNull(emails);

    IReadOnlyList<string>? phones = resource?.Contact?.Phones;
    ArgumentNullException.ThrowIfNull(phones);

    Warehouse warehouse = new
    (
      id: WarehouseId.New(),
      address: new WarehouseAddress
      (
        address: Address.Parse(address),
        coordinates: new Coordinates
        (
          latitude: new Latitude(latitude.Value),
          longitude: new Longitude(longitude.Value)
        )
      ),
      contact: new WarehouseContact
      (
        emails: [.. emails.Select(x => Email.Parse(x))],
        phones: [.. phones.Select(x => Phone.Parse(x))]
      ),
      merchantId: merchantId
    );

    return warehouse;
  }
}
