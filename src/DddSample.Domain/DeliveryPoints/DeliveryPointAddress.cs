namespace DddSample.Domain.DeliveryPoints;

public sealed class DeliveryPointAddress
{
  public DeliveryPointAddress(in Address address, in Coordinates coodinates)
  {
    Address = address;
    Coodinates = coodinates;
  }

  public Address Address { get; }

  public Coordinates Coodinates { get; }
}
