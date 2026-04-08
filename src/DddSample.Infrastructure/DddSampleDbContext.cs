using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure;

internal sealed class DddSampleDbContext(DbContextOptions options) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new MerchantEntityTypeConfiguration());
  }
}
