using DddSample.Domain;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Resources.Warehouses;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DddSample.Resources.Merchants;

internal static class MerchantResourceMapping
{
  internal static MerchantResource ToResource(this Merchant merchant)
  {
    return new()
    {
      Id = merchant.Id.ToString(),
      Name = merchant.Name,
      Deleted = merchant.Deleted,
    };
  }

  internal static Merchant ToEntity(this MerchantResource resource)
  {
    string? name = resource.Name;
    ArgumentNullException.ThrowIfNull(name);

    Merchant merchant = new
    (
      id: MerchantId.New(),
      name: name
    );

    return merchant;
  }

  internal static Merchant ToEntity(this MerchantResource resource, MerchantId merchantId)
  {
    string? name = resource.Name;
    ArgumentNullException.ThrowIfNull(name);

    Merchant merchant = new
    (
      id: merchantId,
      name: name
    );

    return merchant;
  }
}
