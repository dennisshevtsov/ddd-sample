using DddSample.Infrastructure.DeliveryPoints;
using DddSample.Infrastructure.Merchants;
using DddSample.Infrastructure.Warehouses;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DddSample.Infrastructure;

internal sealed class DddSampleDbContext(DbContextOptions options) : DbContext(options)
{
  private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new AddressJsonConverter(),
      new CoordinatesJsonConverter(),
      new DeliveryPointAddressJsonConverter(),
      new DeliveryPointOpeningHoursJsonConverter(),
      new EmailJsonConverter(),
      new LatitudeJsonConverter(),
      new LongitudeJsonConverter(),
      new PhoneJsonConverter(),
      new TimePeriodJsonConverter(),
      new WarehouseAddressJsonConverter(),
      new WarehouseContactJsonConverter(),
    },
  };

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new DeliveryPointEntityTypeConfiguration(_jsonSerializerOptions));
    modelBuilder.ApplyConfiguration(new MerchantEntityTypeConfiguration());
    modelBuilder.ApplyConfiguration(new WarehouseEntityTypeConfiguration(_jsonSerializerOptions));
  }
}
