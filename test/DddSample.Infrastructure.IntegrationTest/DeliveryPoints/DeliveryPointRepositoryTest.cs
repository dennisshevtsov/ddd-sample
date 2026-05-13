using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure.IntegrationTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddSample.Infrastructure.DeliveryPoints.IntegrationTest;

public sealed class DeliveryPointRepositoryTest : IClassFixture<DddSampleWebApplicationFactory>, IAsyncLifetime
{
  private readonly IServiceScope _scope;
  private readonly DbContext _context;
  private readonly IUnitOfWork _uow;
  private readonly IDeliveryPointRepository _deliveryPointRepository;

  private readonly MerchantBuilder _merchantBuilder;
  private readonly WarehouseBuilder _warehouseBuilder;
  private readonly DeliveryPointBuilder _deliveryPointBuilder;

  public DeliveryPointRepositoryTest(DddSampleWebApplicationFactory factory)
  {
    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _uow = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    _deliveryPointRepository = _scope.ServiceProvider.GetRequiredService<IDeliveryPointRepository>();

    _merchantBuilder = MerchantBuilder.Default();
    _warehouseBuilder = WarehouseBuilder.Default();
    _deliveryPointBuilder = DeliveryPointBuilder.Default();
  }

  public async ValueTask InitializeAsync()
  {
    await _context.Database.EnsureCreatedAsync();

    Merchant merchant = _merchantBuilder.Build();
    _context.Add(merchant);

    Warehouse warehouse = _warehouseBuilder.MerchantId(merchant.Id)
                                           .Build();
    _context.Add(warehouse);

    await _context.SaveChangesAsync();
  }

  public async ValueTask DisposeAsync()
  {
    try
    {
      await _context.Set<DeliveryPoint>().ExecuteDeleteAsync();
      await _context.Set<Warehouse>().ExecuteDeleteAsync();
      await _context.Set<Merchant>().ExecuteDeleteAsync();
    }
    finally
    {
      _scope?.Dispose();
    }
  }

  [Fact(DisplayName = "When a new instance of class DeliveryPoint added, a new record is saved to the DB")]
  public async Task CommitAsync_NewDeliveryPoint_DeliveryPointSaved()
  {
    // Arrange
    DeliveryPoint deliveryPointToSave = _deliveryPointBuilder.WarehouseId(_warehouseBuilder.WarehouseId)
                                                             .Build();
    _deliveryPointRepository.Add(deliveryPointToSave);

    // Act
    await _uow.CommitAsync(TestContext.Current.CancellationToken);

    // Assert
    DeliveryPoint? deliveryPointInDb = await _context.Set<DeliveryPoint>()
                                                     .AsNoTracking()
                                                     .SingleOrDefaultAsync(TestContext.Current.CancellationToken);
    Assert.NotNull(deliveryPointInDb);
  }
}
