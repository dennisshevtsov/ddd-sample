using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct WarehouseId : IEquatable<WarehouseId>
{
  public static readonly WarehouseId None;

  private readonly Guid _id;

  private WarehouseId(Guid id) => _id = id;

  public override int GetHashCode() => _id.GetHashCode();

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is null) return false;
    if (obj is not WarehouseId id) return false;
    return Equals(id);
  }

  public bool Equals(WarehouseId other) => _id == other._id;

  public override string ToString() => _id.ToString();

  public static WarehouseId New() => new(Guid.CreateVersion7());
  public static WarehouseId Parce(string value) => new(Guid.Parse(value));

  public static implicit operator WarehouseId(Guid value) => new(value);
  public static explicit operator Guid(WarehouseId value) => value._id;

  public static bool operator ==(WarehouseId left, WarehouseId right) => left.Equals(right);
  public static bool operator !=(WarehouseId left, WarehouseId right) => !left.Equals(right);
}

