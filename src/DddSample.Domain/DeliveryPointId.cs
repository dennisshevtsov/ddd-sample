namespace DddSample.Domain;

public readonly struct DeliveryPointId
{
  private readonly Guid _id;

  private DeliveryPointId(Guid id) => _id = id;

  public static readonly DeliveryPointId None;

  public static DeliveryPointId New() => new(Guid.CreateVersion7());
  public static DeliveryPointId Parce(string value) => new(Guid.Parse(value));

  public static implicit operator DeliveryPointId(Guid value) => new(value);
  public static explicit operator Guid(DeliveryPointId value) => value._id;
}

