namespace DddSample.Domain;

public sealed class DeliveryPoint
{
  public DeliveryPoint(DeliveryPointId id, DeliveryPointAddress address, WeeklyOpeningHours openingHours)
  {
    Id = id;
    Address = address;
    OpeningHours = openingHours;
  }

  public DeliveryPointId Id { get; }
  public DeliveryPointAddress Address { get; }
  public WeeklyOpeningHours OpeningHours { get; }
}
