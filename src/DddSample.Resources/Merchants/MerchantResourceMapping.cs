using DddSample.Domain;
using DddSample.Domain.Merchants;

namespace DddSample.Resources.Merchants;

internal static class MerchantResourceMapping
{
  internal static MerchantResource ToResource(this Merchant merchant)
  {
    return new()
    {
      Id = merchant.Id.ToString(),
      Name = merchant.Name,
      DeliveryPointId = merchant.DeliveryPointId.ToString(),
      Deleted = merchant.Deleted,
    };
  }

  internal static Merchant ToEntity(this MerchantResource resource)
  {
    return resource.ToEntity(merchantId: MerchantId.New());
  }

  internal static Merchant ToEntity(this MerchantResource resource, MerchantId merchantId)
  {
    string? name = resource.Name;
    ArgumentNullException.ThrowIfNull(name);

    ArgumentNullException.ThrowIfNull(resource.DeliveryPointId);
    DeliveryPointId deliveryPointId = DeliveryPointId.Parce(resource.DeliveryPointId);

    Merchant merchant = new
    (
      id: merchantId,
      name: name,
      deliveryPointId: deliveryPointId
    );

    return merchant;
  }
}
