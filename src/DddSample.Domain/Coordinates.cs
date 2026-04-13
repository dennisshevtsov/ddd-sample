using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct Coordinates : IEquatable<Coordinates>
{
  public Coordinates(in Latitude latitude, in Longitude longitude)
  {
    Latitude = latitude;
    Longitude = longitude;
  }

  public Latitude Latitude { get; }

  public Longitude Longitude { get; }

  public static readonly Coordinates None;

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj == null) return false;
    if (obj is not Coordinates other) return false;
    return Equals(other);
  }

  public bool Equals(Coordinates other) => Latitude == other.Latitude && Longitude == other.Longitude;

  public override int GetHashCode() => HashCode.Combine(Latitude.GetHashCode(), Longitude.GetHashCode());

  public override string ToString() => $"{{ latitude: {Latitude}, longitude: {Longitude} }}";

  public static bool operator ==(Coordinates left, Coordinates right) => Equals(left, right);
  public static bool operator !=(Coordinates left, Coordinates right) => Equals(left, right);
}
