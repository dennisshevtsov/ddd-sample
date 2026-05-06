using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.DeliveryPoints;

internal sealed class DeliveryPointRepository(EfUnitOfWork uow) : IDeliveryPointRepository
{
  public async Task<DeliveryPoint?> GetAsync(DeliveryPointId id, CancellationToken cancellationToken = default)
  {
    return await uow.AsQueryable<DeliveryPoint>()
                    .Where(deliveryPoint => deliveryPoint.Id == id)
                    .FirstOrDefaultAsync();
  }

  public void Add(DeliveryPoint deliveryPoint) => uow.Add(deliveryPoint);

  public void Remove(DeliveryPoint deliveryPoint) => uow.Remove(deliveryPoint);
}
