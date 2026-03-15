using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct Longitude : IEquatable<Longitude>
{
  private const double MinValue = -180D;
  private const double MaxValue = 180D;

  private readonly double _longitude;

  private Longitude(double longitude)
  {
    if (longitude < MinValue)
    {
      throw new DomainException($"Invalid longitude: {longitude}. Longitude must be >= {MinValue}");
    }
    if (longitude > MaxValue)
    {
      throw new DomainException($"Invalid longitude: {longitude}. Longitude must be <= {MaxValue}");
    }
    _longitude = longitude;
  }

  public static readonly Longitude Zero;

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is null) return false;
    if (obj is not Longitude other) return false;
    return Equals(other);
  }

  public bool Equals(Longitude other) => _longitude == other._longitude;

  public override int GetHashCode() => _longitude.GetHashCode();

  public override string ToString() => _longitude.ToString();

  public static implicit operator double(Longitude longitude) => longitude._longitude;
  public static explicit operator Longitude(double value) => new(value);
  public static bool operator ==(Longitude left, Longitude right) => left.Equals(right);
  public static bool operator !=(Longitude left, Longitude right) => !left.Equals(right);
}
