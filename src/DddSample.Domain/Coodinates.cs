namespace DddSample.Domain;

public readonly struct Coodinates
{
  public Coodinates(in Latitude latitude, in Longitude longitude)
  {
    Latitude = latitude;
    Longitude = longitude;
  }

  public Latitude Latitude { get; }

  public Longitude Longitude { get; }

  public static readonly Coodinates None;
}
