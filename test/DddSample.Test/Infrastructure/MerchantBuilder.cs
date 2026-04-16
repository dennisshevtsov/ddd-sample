using DddSample.Domain;
using DddSample.Domain.Merchants;

namespace DddSample.Infrastructure.Test;

internal sealed class MerchantBuilder
{
  private MerchantId _id;
  private string? _name;

  internal MerchantId MerchantId => _id;

  internal MerchantBuilder Id(in MerchantId id)
  {
    _id = id;
    return this;
  }

  internal MerchantBuilder Name(string? name)
  {
    _name = name;
    return this;
  }

  internal Merchant Build()
  {
    return new
    (
      id: _id,
      name: _name
    );
  }
}
