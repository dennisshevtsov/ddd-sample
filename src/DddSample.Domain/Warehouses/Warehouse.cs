namespace DddSample.Domain.Warehouses;

public sealed class Warehouse
{
  public Warehouse(WarehouseId id, WarehouseAddress address, WarehouseContact contact, MerchantId merchantId)
  {
    Id = id;
    Address = address ?? throw new ArgumentNullException(nameof(address));
    Contact = contact ?? throw new ArgumentNullException(nameof(contact));
    MerchantId = merchantId;
  }

  public WarehouseId Id { get; }

  public WarehouseAddress Address { get; }

  public WarehouseContact Contact { get; }

  public MerchantId MerchantId { get; }
}

