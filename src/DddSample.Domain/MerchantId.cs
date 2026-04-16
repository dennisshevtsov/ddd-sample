using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct MerchantId : IEquatable<MerchantId>
{
  public static readonly MerchantId None;

  private readonly Guid _id;

  private MerchantId(Guid id) => _id = id;

  public override int GetHashCode() => _id.GetHashCode();

  public bool Equals(MerchantId other) => _id == other._id;

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is null) return false;
    if (obj is not MerchantId other) return false;
    return Equals(other);
  }

  public override string ToString() => _id.ToString();

  public static MerchantId New() => new(Guid.CreateVersion7());
  public static MerchantId Parce(string value) => new(Guid.Parse(value));

  public static implicit operator MerchantId(Guid value) => new(value);
  public static explicit operator Guid(MerchantId value) => value._id;

  public static bool operator ==(MerchantId left, MerchantId right) => Equals(left, right);
  public static bool operator !=(MerchantId left, MerchantId right) => !Equals(left, right);
}
