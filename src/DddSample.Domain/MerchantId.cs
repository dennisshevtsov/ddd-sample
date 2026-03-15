namespace DddSample.Domain;

public readonly struct MerchantId
{
  private readonly Guid _id;

  private MerchantId(Guid id) => _id = id;

  public static readonly MerchantId None;

  public static MerchantId New() => new(Guid.CreateVersion7());
  public static MerchantId Parce(string value) => new(Guid.Parse(value));

  public static implicit operator MerchantId(Guid value) => new(value);
  public static explicit operator Guid(MerchantId value) => value._id;
}
