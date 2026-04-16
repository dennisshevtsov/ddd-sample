using DddSample.Domain;
using DddSample.Domain.Warehouses;

namespace DddSample.Infrastructure.Test;

internal sealed class WarehouseBuilder
{
  private WarehouseId _id;
  private Address _address;
  private Coordinates _coordinates;
  private List<Email> _emails = new();
  private List<Phone> _phones = new();
  private MerchantId _merchantId;

  internal WarehouseId WarehouseId => _id;

  internal WarehouseBuilder Id(in WarehouseId id)
  {
    _id = id;
    return this;
  }

  internal WarehouseBuilder Address(in Address address)
  {
    _address = address;
    return this;
  }

  internal WarehouseBuilder Coordinates(in Coordinates coordinates)
  {
    _coordinates = coordinates;
    return this;
  }

  internal WarehouseBuilder Email(in Email email)
  {
    _emails.Add(email);
    return this;
  }

  internal WarehouseBuilder Phone(in Phone phone)
  {
    _phones.Add(phone);
    return this;
  }

  internal WarehouseBuilder MerchantId(in MerchantId merchantId)
  {
    _merchantId = merchantId;
    return this;
  }

  internal Warehouse Build()
  {
    return new Warehouse
    (
      id: _id,
      address: new WarehouseAddress
      (
        address: _address,
        coordinates: _coordinates
      ),
      contact: new WarehouseContact
      (
        emails: _emails,
        phones: _phones
      ),
      merchantId: _merchantId
    );
  }
}
