namespace DddSample.Domain;

public readonly struct Latitude
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

  public static implicit operator double(Latitude latitude) => latitude._latitude;
  public static explicit operator Latitude(double value) => new(value);
}
