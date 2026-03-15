namespace DddSample.Domain;

public sealed class DeliveryPointAddress
{
  public DeliveryPointAddress(in Address address, in Coodinates coodinates)
  {
    Address = address;
    Coodinates = coodinates;
  }

  public Address Address { get; }

  public Coodinates Coodinates { get; }
}
