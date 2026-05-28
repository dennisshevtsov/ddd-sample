using DddSample.Resources.Merchants;
using Refit;

namespace DddSample.IntegrationTests.Resources;

public interface IMerchantApi
{
  [Get("/api/v1/merchants/{id}")]
  public Task<MerchantResource> Get(string id);

  [Post("/api/v1/merchants")]
  public Task<MerchantResource> Create(string id);
}
