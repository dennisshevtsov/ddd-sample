using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Test.Infrastructure;

[TestClass]
[TestCategory("Integration")]
public sealed class DeliveryPointTest
{
  private IServiceScope _scope;
  private DbContext _context1;
  private DbContext _context2;

  public TestContext TestContext { get; set; }

  [TestInitialize]
  public async Task InitializeAsync()
  {
    DddSampleWebApplicationFactory factory = new();

    _scope = factory.Services.CreateScope();
    _context1 = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _context2 = _scope.ServiceProvider.GetRequiredService<DbContext>();

    await _context1.Database.EnsureCreatedAsync();
  }

  [TestCleanup]
  public async Task CleanupAsync()
  {
    try
    {
      await _context1.Database.EnsureDeletedAsync();
    }
    finally
    {
      _scope?.Dispose();
    }
  }

  [TestMethod]
  [Timeout(5000, CooperativeCancellation = true)]
  public async Task SaveChangesAsync_NewMerchant_MerchantSaved()
  {
    // Arrange
    MerchantId merchantId = MerchantId.New();
    Merchant merchantToSave = new
    (
      id: merchantId,
      name: "test merchant name"
    );

    WarehouseId warehouseId = WarehouseId.New();
    Warehouse warehouseToSave = new
    (
      id: warehouseId,
      address: new WarehouseAddress
      (
        address: Address.Parse("test address"),
        coodinates: new Coordinates
        (
          latitude: new Latitude(1D),
          longitude: new Longitude(2D)
        )
      ),
      contact: new WarehouseContact
      (
        phones: [Phone.Parse("375331234567")],
        emails: [Email.Parse("test@test")]
      ),
      merchantId
    );

    DeliveryPointId deliveryPointId = DeliveryPointId.New();
    DeliveryPoint deliveryPointToSave = new
    (
      id: deliveryPointId,
      address: new DeliveryPointAddress
      (
        address: Address.Parse("test address"),
        coordinates: new Coordinates
        (
          latitude: new Latitude(1D),
          longitude: new Longitude(2D)
        )
      ),
      openingHours: new DeliveryPointOpeningHours
      (
        worksOnHolidays: true,
        mon: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        tue: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        wed: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        thu: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        fri: new TimePeriod(from: new TimeOnly(hour: 09, minute: 00), to: new TimeOnly(hour: 22, minute: 00)),
        sat: new TimePeriod(from: new TimeOnly(hour: 10, minute: 00), to: new TimeOnly(hour: 20, minute: 30)),
        sun: new TimePeriod(from: new TimeOnly(hour: 10, minute: 00), to: new TimeOnly(hour: 20, minute: 30))
      ),
      warehouseId
    );

    _context1.Add(merchantToSave);
    _context1.Add(warehouseToSave);
    _context1.Add(deliveryPointToSave);

    // Act
    await _context1.SaveChangesAsync(TestContext.CancellationToken);

    // Assert
    DeliveryPoint? deliveryPointInDb = await _context2.Set<DeliveryPoint>()
                                                      .AsNoTracking()
                                                      .SingleOrDefaultAsync(TestContext.CancellationToken);
    Assert.IsNotNull(deliveryPointInDb);
  }
}
