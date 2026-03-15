namespace DddSample.Domain.Warehouses;

public interface IWarehouseRepository
{
  public Task<Warehouse?> GetAsync(MerchantId id, CancellationToken cancellationToken = default);
  public Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
  public Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
  public Task CommitAsync(CancellationToken cancellationToken = default);
}
