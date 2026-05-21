using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;

namespace DddSample.Resources.DeliveryPoints;

internal static class DeliveryPointMapping
{
  internal static DeliveryPointResource ToResource(this DeliveryPoint deliveryPoint)
  {
    return new()
    {
      Id = deliveryPoint.Id.ToString(),
      WarehouseId = deliveryPoint.WarehouseId.ToString(),
      Address = new DeliveryPointAddressResource
      {
        Address = deliveryPoint.Address.Address.ToString(),
        Coordinates = new CoordinatesResource
        {
          Latitude = deliveryPoint.Address.Coordinates.Latitude,
          Longitude = deliveryPoint.Address.Coordinates.Longitude,
        },
      },
      OpeningHours = new DeliveryPointOpeningHoursResource
      {
        WorksOnHolidays = deliveryPoint.OpeningHours.WorksOnHolidays,
        Mon = deliveryPoint.OpeningHours.Mon.ToString(),
        Tue = deliveryPoint.OpeningHours.Tue.ToString(),
        Wed = deliveryPoint.OpeningHours.Wed.ToString(),
        Thu = deliveryPoint.OpeningHours.Thu.ToString(),
        Fri = deliveryPoint.OpeningHours.Fri.ToString(),
        Sat = deliveryPoint.OpeningHours.Sat.ToString(),
        Sun = deliveryPoint.OpeningHours.Sun.ToString(),
      },
    };
  }

  internal static DeliveryPoint ToEntity(this DeliveryPointResource resource, WarehouseId warehouseId)
  {
    string? address = resource.Address?.Address;
    ArgumentNullException.ThrowIfNull(address);

    double? latitude = resource.Address?.Coordinates?.Latitude;
    ArgumentNullException.ThrowIfNull(latitude);

    double? longitude = resource.Address?.Coordinates?.Longitude;
    ArgumentNullException.ThrowIfNull(longitude);

    bool? worksOnHolidays = resource.OpeningHours?.WorksOnHolidays;
    ArgumentNullException.ThrowIfNull(worksOnHolidays);

    string? mon = resource.OpeningHours?.Mon;
    ArgumentNullException.ThrowIfNull(mon);

    string? tue = resource.OpeningHours?.Tue;
    ArgumentNullException.ThrowIfNull(tue);

    string? wed = resource.OpeningHours?.Wed;
    ArgumentNullException.ThrowIfNull(wed);

    string? thu = resource.OpeningHours?.Thu;
    ArgumentNullException.ThrowIfNull(thu);

    string? fri = resource.OpeningHours?.Fri;
    ArgumentNullException.ThrowIfNull(fri);

    string? sat = resource.OpeningHours?.Sat;
    ArgumentNullException.ThrowIfNull(sat);

    string? sun = resource.OpeningHours?.Sun;
    ArgumentNullException.ThrowIfNull(sun);

    return new DeliveryPoint
    (
      id: DeliveryPointId.New(),
      address: new DeliveryPointAddress
      (
        address: Address.Parse(address),
        coordinates: new Coordinates(new Latitude(latitude.Value), new Longitude(longitude.Value))
      ),
      openingHours: new DeliveryPointOpeningHours
      (
        worksOnHolidays: worksOnHolidays.Value,
        mon: TimePeriod.Parse(mon),
        tue: TimePeriod.Parse(tue),
        wed: TimePeriod.Parse(wed),
        thu: TimePeriod.Parse(thu),
        fri: TimePeriod.Parse(fri),
        sat: TimePeriod.Parse(sat),
        sun: TimePeriod.Parse(sun)
      ),
      warehouseId: warehouseId
    );
  }
}
