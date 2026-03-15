using DddSample.Domain;
using DddSample.Domain.Warehouses;

namespace DddSample.Infrastructure;

public sealed class WarehouseRepository : IWarehouseRepository
{
  public Task<Warehouse?> GetAsync(MerchantId id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task CommitAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
