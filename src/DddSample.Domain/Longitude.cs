namespace DddSample.Domain;

public readonly struct Longitude
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

  public static implicit operator double(Longitude longitude) => longitude._longitude;
  public static explicit operator Longitude(double value) => new(value);
}
