namespace DddSample.Domain.DeliveryPoints;

public sealed class DeliveryPointAddress
{
  public DeliveryPointAddress(in Address address, in Coordinates coordinates)
  {
    Address = address;
    Coordinates = coordinates;
  }

  public Address Address { get; }

  public Coordinates Coordinates { get; }
}
