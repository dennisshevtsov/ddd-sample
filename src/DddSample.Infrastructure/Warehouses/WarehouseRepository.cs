using DddSample.Domain;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Warehouses;

internal sealed class WarehouseRepository(EfUnitOfWork uow) : IWarehouseRepository
{
  public Task<Warehouse?> GetAsync(WarehouseId id, CancellationToken cancellationToken = default)
  {
    return uow.AsQueryable<Warehouse>()
              .Where(warehouse => warehouse.Id == id)
              .FirstOrDefaultAsync(cancellationToken);
  }

  public void Add(Warehouse warehouse) => uow.Add(warehouse);

  public void Remove(Warehouse warehouse) => uow.Remove(warehouse);
}
