namespace DddSample.Domain.DeliveryPoints;

public interface IDeliveryPointRepository
{
  public Task<DeliveryPoint?> GetAsync(DeliveryPointId id, CancellationToken cancellationToken = default);
  public Task AddAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default);
  public Task DeleteAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default);
  public Task CommitAsync(CancellationToken cancellationToken = default);
}
