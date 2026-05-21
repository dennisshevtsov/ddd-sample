namespace DddSample.Domain.Merchants;

public sealed class Merchant : IAggregate
{
  public Merchant(MerchantId id, string name, DeliveryPointId deliveryPointId)
  {
    Id = id;
    Name = name;
    DeliveryPointId = deliveryPointId;
  }

  public MerchantId Id { get; }

  public string Name { get; private set; }

  public bool Deleted { get; private set; }

  public DeliveryPointId DeliveryPointId { get; private set; }

  public void Replace(string name, DeliveryPointId deliveryPointId)
  {
    Name = name;
    DeliveryPointId = DeliveryPointId;
  }

  public void Delete()
  {
    Deleted = true;
  }
}
