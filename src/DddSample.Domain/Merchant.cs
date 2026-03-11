namespace DddSample.Domain;

public sealed class Merchant : AggregateRoot
{
  public Merchant(MerchantId id, string name)
  {
    Id = id;
    Name = name;
  }

  public MerchantId Id { get; }

  public string? Name { get; private set; }

  public bool Deleted { get; private set; }

  public void Replace(string? name)
  {
    Name = name;
  }

  public void Delete()
  {
    Deleted = true;
  }
}
