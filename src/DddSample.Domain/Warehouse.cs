namespace DddSample.Domain;

public sealed class Warehouse : AggregateRoot
{
  public Warehouse(WarehouseId id, WarehouseAddress address, WarehouseContact contact)
  {
    Id = id;
    Address = address ?? throw new ArgumentNullException(nameof(address));
    Contact = contact ?? throw new ArgumentNullException(nameof(contact));
  }

  public WarehouseId Id { get; }

  public WarehouseAddress Address { get; }

  public WarehouseContact Contact { get; }
}

