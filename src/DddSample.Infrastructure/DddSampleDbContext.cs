using DddSample.Infrastructure.DeliveryPoints;
using DddSample.Infrastructure.Merchants;
using DddSample.Infrastructure.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure;

internal sealed class DddSampleDbContext(DbContextOptions options) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new DeliveryPointEntityTypeConfiguration());
    modelBuilder.ApplyConfiguration(new MerchantEntityTypeConfiguration());
    modelBuilder.ApplyConfiguration(new WarehouseEntityTypeConfiguration());
  }
}
