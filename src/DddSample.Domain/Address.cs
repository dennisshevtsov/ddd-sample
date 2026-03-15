using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct Address : IEquatable<Address>
{
  private readonly string _address;

  private Address(string address) => _address = address;

  public readonly static Address None;

  public static Address Parse(string value)
  {
    ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
    return new Address(value);
  }

  public override int GetHashCode() => _address.GetHashCode();

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj == null) return false;
    if (obj is not Address other) return false;
    return Equals(this, other);
  }

  public bool Equals(Address other) => _address == other._address;

  public override string ToString() => _address;

  public static implicit operator string(Address address) => address.ToString();
  public static bool operator ==(Address left, Address right) => left.Equals(right);
  public static bool operator !=(Address left, Address right) => !left.Equals(right);
}

