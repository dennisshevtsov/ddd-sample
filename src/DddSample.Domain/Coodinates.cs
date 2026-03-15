using System.Diagnostics.CodeAnalysis;

namespace DddSample.Domain;

public readonly struct Coodinates : IEquatable<Coodinates>
{
  public Coodinates(in Latitude latitude, in Longitude longitude)
  {
    Latitude = latitude;
    Longitude = longitude;
  }

  public Latitude Latitude { get; }

  public Longitude Longitude { get; }

  public static readonly Coodinates None;

  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    if (obj == null) return false;
    if (obj is not Coodinates other) return false;
    return Equals(other);
  }

  public bool Equals(Coodinates other) => Latitude == other.Latitude && Longitude == other.Longitude;

  public override int GetHashCode() => HashCode.Combine(Latitude.GetHashCode(), Longitude.GetHashCode());

  public override string ToString() => $"{{ latitude: {Latitude}, longitude: {Longitude} }}";

  public static bool operator ==(Coodinates left, Coodinates right) => Equals(left, right);
  public static bool operator !=(Coodinates left, Coodinates right) => Equals(left, right);
}
