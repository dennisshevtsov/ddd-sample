using DddSample.Resources.DeliveryPoints;
using Refit;

namespace DddSample.IntegrationTests.Resources;

public interface IDeliveryPointApi
{
  [Get("/api/v1/warehouses/{warehouseId}/delivery-points/{id}")]
  public Task<DeliveryPointResource> Get(string warehouseId, string id);

  [Post("/api/v1/warehouses/{warehouseId}/delivery-points")]
  public Task<DeliveryPointResource> Create(string warehouseId, DeliveryPointResource resource);
}
