using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;

namespace DddSample.Infrastructure;

public sealed class DeliveryPointRepository : IDeliveryPointRepository
{
  public Task<DeliveryPoint?> GetAsync(DeliveryPointId id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task AddAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task CommitAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
