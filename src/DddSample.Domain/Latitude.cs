using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct Latitude : IEquatable<Latitude>
{
  private const double MinValue = -90D;
  private const double MaxValue = 90D;

  private readonly double _latitude;

  private Latitude(double latitude)
  {
    if (latitude < MinValue)
    {
      throw new DomainException($"Invalid latitude: {latitude}. Latitude must be >= {MinValue}");
    }
    if (latitude > MaxValue)
    {
      throw new DomainException($"Invalid latitude: {latitude}. Latitude must be <= {MaxValue}");
    }
    _latitude = latitude;
  }

  public static readonly Latitude Zero;

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj is null) return false;
    if (obj is not Latitude other) return false;
    return Equals(other);
  }

  public bool Equals(Latitude other) => _latitude == other._latitude;

  public override int GetHashCode() => _latitude.GetHashCode();

  public override string ToString() => _latitude.ToString();

  public static implicit operator double(Latitude latitude) => latitude._latitude;
  public static explicit operator Latitude(double value) => new(value);
  public static bool operator ==(Latitude left, Latitude right) => left.Equals(right);
  public static bool operator !=(Latitude left, Latitude right) => !left.Equals(right);
}
