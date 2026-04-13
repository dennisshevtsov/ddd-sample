using DddSample.Domain;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Warehouses;

internal sealed class WarehouseRepository(DbContext dbContext) : IWarehouseRepository
{
  public Task<Warehouse?> GetAsync(WarehouseId id, CancellationToken cancellationToken = default)
  {
    return dbContext.Set<Warehouse>()
                    .Where(warehouse => warehouse.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);
  }

  public void Add(Warehouse warehouse) => dbContext.Add(warehouse);

  public void Delete(Warehouse warehouse)
  {
    dbContext.Set<Warehouse>()
             .Remove(warehouse);
  }

  public Task CommitAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
