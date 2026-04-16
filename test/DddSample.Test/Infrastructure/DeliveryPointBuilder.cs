using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;

namespace DddSample.Infrastructure.Test;

internal sealed class DeliveryPointBuilder
{
  private DeliveryPointId _id;
  private Address _address;
  private Coordinates _coordinates;
  private DeliveryPointOpeningHours? _openingHours;
  private WarehouseId _warehouseId;

  internal DeliveryPointBuilder Id(DeliveryPointId id)
  {
    _id = id;
    return this;
  }

  internal DeliveryPointBuilder Address(Address address)
  {
    _address = address;
    return this;
  }

  internal DeliveryPointBuilder Coordinates(Coordinates coordinates)
  {
    _coordinates = coordinates;
    return this;
  }

  internal DeliveryPointBuilder OpeningHours(DeliveryPointOpeningHours openingHours)
  {
    _openingHours = openingHours;
    return this;
  }

  internal DeliveryPointBuilder WarehouseId(WarehouseId warehouseId)
  {
    _warehouseId = warehouseId;
    return this;
  }

  internal DeliveryPoint Build()
  {
    return new DeliveryPoint
    (
      id: _id,
      address: new DeliveryPointAddress
      (
        address: _address,
        coordinates: _coordinates
      ),
      openingHours: _openingHours ?? new DeliveryPointOpeningHours(),
      warehouseId: _warehouseId
    );
  }

  internal static DeliveryPointBuilder Default()
  {
    return new DeliveryPointBuilder()
      .Id(DeliveryPointId.New())
      .Address(Domain.Address.Parse("test address"))
      .Coordinates(new Coordinates
      (
        latitude: new Latitude(1D),
        longitude: new Longitude(2D)
      ))
      .OpeningHours(new DeliveryPointOpeningHours
      (
        worksOnHolidays: true,
        mon: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        tue: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        wed: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        thu: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        fri: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        sat: new TimePeriod(from: new TimeOnly(hour: 10, minute: 00), to: new TimeOnly(hour: 20, minute: 30)),
        sun: new TimePeriod(from: new TimeOnly(hour: 10, minute: 00), to: new TimeOnly(hour: 20, minute: 30))
      ));
  }
}
