namespace DddSample.Domain;

public readonly struct WarehouseId
{
  private readonly Guid _id;

  private WarehouseId(Guid id) => _id = id;

  public static readonly WarehouseId None;

  public static WarehouseId New() => new(Guid.CreateVersion7());
  public static WarehouseId Parce(string value) => new(Guid.Parse(value));

  public static implicit operator WarehouseId(Guid value) => new(value);
  public static explicit operator Guid(WarehouseId value) => value._id;
}

