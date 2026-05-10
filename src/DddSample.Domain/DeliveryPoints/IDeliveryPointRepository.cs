namespace DddSample.Domain.DeliveryPoints;

public interface IDeliveryPointRepository
{
  public Task<DeliveryPoint?> GetAsync(DeliveryPointId id, CancellationToken cancellationToken = default);
  public void Add(DeliveryPoint deliveryPoint);
  public void Remove(DeliveryPoint deliveryPoint);
}
