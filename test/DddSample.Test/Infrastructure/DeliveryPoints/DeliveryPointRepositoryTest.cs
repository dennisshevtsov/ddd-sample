using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure.Test;
using DddSample.Test;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.DeliveryPoints.Test;

[TestClass]
[TestCategory("Integration")]
public sealed class DeliveryPointRepositoryTest
{
  private IServiceScope _scope;
  private DbContext _context;
  private IDeliveryPointRepository _deliveryPointRepository;

  private MerchantBuilder _merchantBuilder;
  private WarehouseBuilder _warehouseBuilder;
  private DeliveryPointBuilder _deliveryPointBuilder;

  public TestContext TestContext { get; set; }

  [TestInitialize]
  public async Task InitializeAsync()
  {
    DddSampleWebApplicationFactory factory = new();

    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _deliveryPointRepository = _scope.ServiceProvider.GetRequiredService<IDeliveryPointRepository>();

    await _context.Database.EnsureCreatedAsync();

    _merchantBuilder = MerchantBuilder.Default();
    Merchant merchant = _merchantBuilder.Build();
    _context.Add(merchant);

    _warehouseBuilder = WarehouseBuilder.Default()
                                        .MerchantId(merchant.Id);
    Warehouse warehouse = _warehouseBuilder.Build();
    _context.Add(warehouse);

    _deliveryPointBuilder = DeliveryPointBuilder.Default();

    await _context.SaveChangesAsync();
  }

  [TestCleanup]
  public async Task CleanupAsync()
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

  [TestMethod(DisplayName = "When a new instance of class DeliveryPoint added, a new record is saved to the DB")]
  [Timeout(5000, CooperativeCancellation = true)]
  public async Task CommitAsync_NewDeliveryPoint_DeliveryPointSaved()
  {
    // Arrange
    DeliveryPoint deliveryPointToSave = _deliveryPointBuilder.WarehouseId(_warehouseBuilder.WarehouseId)
                                                             .Build();

    _deliveryPointRepository.Add(deliveryPointToSave);

    // Act
    await _deliveryPointRepository.CommitAsync(TestContext.CancellationToken);

    // Assert
    DeliveryPoint? deliveryPointInDb = await _context.Set<DeliveryPoint>()
                                                     .AsNoTracking()
                                                     .SingleOrDefaultAsync(TestContext.CancellationToken);
    Assert.IsNotNull(deliveryPointInDb);
  }
}
