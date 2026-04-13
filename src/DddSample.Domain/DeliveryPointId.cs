using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct DeliveryPointId : IEquatable<DeliveryPointId>
{
  public static readonly DeliveryPointId None;

  private readonly Guid _id;

  private DeliveryPointId(Guid id) => _id = id;

  public override int GetHashCode() => _id.GetHashCode();

  public bool Equals(DeliveryPointId other) => _id == other._id;

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is null) return false;
    if (obj is not DeliveryPointId other) return false;
    return Equals(other);
  }

  public override string ToString() => _id.ToString();

  public static DeliveryPointId New() => new(Guid.CreateVersion7());
  public static DeliveryPointId Parce(string value) => new(Guid.Parse(value));

  public static implicit operator DeliveryPointId(Guid value) => new(value);
  public static explicit operator Guid(DeliveryPointId value) => value._id;

  public static bool operator ==(DeliveryPointId left, DeliveryPointId right) => Equals(left, right);
  public static bool operator !=(DeliveryPointId left, DeliveryPointId right) => !Equals(left, right);
}

