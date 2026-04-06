using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure;

internal sealed class DddSampleDbContext : DbContext
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new MerchantEntityTypeConfiguration());
  }
}
