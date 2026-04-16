using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure.Test;
using DddSample.Test;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Warehouses.Test;

[TestClass]
[TestCategory("Integration")]
public sealed class WarehouseRepositoryTest
{
  private IServiceScope _scope;
  private DbContext _context;
  private IWarehouseRepository _warehouseRepository;

  private MerchantBuilder _merchantBuilder;
  private WarehouseBuilder _warehouseBuilder;

  public TestContext TestContext { get; set; }

  [TestInitialize]
  public async Task InitializeAsync()
  {
    DddSampleWebApplicationFactory factory = new();

    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _warehouseRepository = _scope.ServiceProvider.GetRequiredService<IWarehouseRepository>();

    await _context.Database.EnsureCreatedAsync();

    _merchantBuilder = MerchantBuilder.Default();
    Merchant merchant = _merchantBuilder.Build();
    _context.Add(merchant);
    await _context.SaveChangesAsync();

    _warehouseBuilder = WarehouseBuilder.Default()
                                        .MerchantId(merchant.Id);
  }

  [TestCleanup]
  public async Task CleanupAsync()
  {
    try
    {
      await _context.Database.EnsureDeletedAsync();
    }
    finally
    {
      _scope?.Dispose();
    }
  }

  [TestMethod(DisplayName = "When a new instance of class Warehouse added, a new record is saved to the DB")]
  [Timeout(5000, CooperativeCancellation = true)]
  public async Task CommitAsync_NewWarehouse_WarehouseSaved()
  {
    // Arrange
    Warehouse warehouseToSave = _warehouseBuilder.Build();
    _warehouseRepository.Add(warehouseToSave);

    // Act
    await _warehouseRepository.CommitAsync(TestContext.CancellationToken);

    // Assert
    Warehouse expected = _warehouseBuilder.Build();
    Warehouse? actual = await _context.Set<Warehouse>()
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(TestContext.CancellationToken);

    Assert.IsNotNull(actual);
    Assert.AreEqual(expected.Id, actual.Id);
    Assert.AreEqual(expected.MerchantId, actual.MerchantId);

    Assert.IsNotNull(actual.Address);
    Assert.AreEqual(expected.Address.Address, actual.Address.Address);
    Assert.AreEqual(expected.Address.Coordinates, actual.Address.Coordinates);

    Assert.IsNotNull(actual.Contact);
    Assert.IsNotNull(actual.Contact.Phones);
    Assert.HasCount(1, actual.Contact.Phones);
    Assert.AreEqual(expected.Contact.Phones[0], actual.Contact.Phones[0]);

    Assert.IsNotNull(actual.Contact);
    Assert.IsNotNull(actual.Contact.Emails);
    Assert.HasCount(1, actual.Contact.Emails);
    Assert.AreEqual(expected.Contact.Emails[0], actual.Contact.Emails[0]);
  }
}
