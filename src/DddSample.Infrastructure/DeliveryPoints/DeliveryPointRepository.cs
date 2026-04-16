using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.DeliveryPoints;

public sealed class DeliveryPointRepository(DbContext context) : IDeliveryPointRepository
{
  public async Task<DeliveryPoint?> GetAsync(DeliveryPointId id, CancellationToken cancellationToken = default)
  {
    return await context.Set<DeliveryPoint>()
                        .Where(deliveryPoint => deliveryPoint.Id == id)
                        .FirstOrDefaultAsync();
  }

  public void Add(DeliveryPoint deliveryPoint) => context.Add(deliveryPoint);

  public void Delete(DeliveryPoint deliveryPoint) => context.Remove(deliveryPoint);

  public Task CommitAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);
}
