using DddSample.Domain;
using DddSample.Domain.Merchants;

namespace DddSample.Infrastructure.IntegrationTests;

internal sealed class MerchantBuilder
{
  private MerchantId _id;
  private string _name = "";
  private DeliveryPointId _deliveryPointId;

  internal MerchantId MerchantId => _id;

  internal MerchantBuilder Id(in MerchantId id)
  {
    _id = id;
    return this;
  }

  internal MerchantBuilder Name(string name)
  {
    _name = name;
    return this;
  }

  internal MerchantBuilder DeliveryPointId(DeliveryPointId deliveryPointId)
  {
    _deliveryPointId = deliveryPointId;
    return this;
  }

  internal Merchant Build()
  {
    return new
    (
      id: _id,
      name: _name,
      deliveryPointId: _deliveryPointId
    );
  }

  internal static MerchantBuilder Default()
  {
    return new MerchantBuilder().Id(MerchantId.New())
                                .Name("test merchant name");
  }
}
