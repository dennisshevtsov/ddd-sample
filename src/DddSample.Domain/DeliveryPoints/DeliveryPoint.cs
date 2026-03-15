namespace DddSample.Domain.DeliveryPoints;

public sealed class DeliveryPoint
{
  public DeliveryPoint(DeliveryPointId id, DeliveryPointAddress address, WeeklyOpeningHours openingHours, WarehouseId warehouseId)
  {
    Id = id;
    Address = address;
    OpeningHours = openingHours;
    WarehouseId = warehouseId;
  }

  public DeliveryPointId Id { get; }
  public DeliveryPointAddress Address { get; }
  public WeeklyOpeningHours OpeningHours { get; }

  public WarehouseId WarehouseId { get; }
}
