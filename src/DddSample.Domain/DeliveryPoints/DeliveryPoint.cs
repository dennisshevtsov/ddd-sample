namespace DddSample.Domain.DeliveryPoints;

public sealed class DeliveryPoint : IAggregate
{
  public DeliveryPoint(DeliveryPointId id, DeliveryPointAddress address, DeliveryPointOpeningHours openingHours, WarehouseId warehouseId)
  {
    Id = id;
    Address = address;
    OpeningHours = openingHours;
    WarehouseId = warehouseId;
  }

  public DeliveryPointId Id { get; }
  public DeliveryPointAddress Address { get; }
  public DeliveryPointOpeningHours OpeningHours { get; }

  public WarehouseId WarehouseId { get; }
}
