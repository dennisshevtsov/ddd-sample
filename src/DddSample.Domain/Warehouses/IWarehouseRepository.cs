namespace DddSample.Domain.Warehouses;

public interface IWarehouseRepository
{
  public Task<Warehouse?> GetAsync(WarehouseId id, CancellationToken cancellationToken = default);
  public void Add(Warehouse warehouse);
  public void Delete(Warehouse warehouse);
  public Task CommitAsync(CancellationToken cancellationToken = default);
}
