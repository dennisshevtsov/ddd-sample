namespace DddSample.Domain.Warehouses;

public sealed class WarehouseAddress
{
  public WarehouseAddress(in Address address, in Coordinates coordinates)
  {
    Address = address;
    Coordinates = coordinates;
  }

  public Address Address { get; }

  public Coordinates Coordinates { get; }
}
