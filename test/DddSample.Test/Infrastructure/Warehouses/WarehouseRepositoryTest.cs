using DddSample.Domain;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Test;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Warehouses.Test;

[TestClass]
[TestCategory("Integration")]
public sealed class WarehouseRepositoryTest
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
    string address = "test address";
    double latitude = 1D;
    double longitude = 2D;
    string phone = "375331234567";
    string email = "test@test";
    Warehouse warehouseToSave = new
    (
      id: warehouseId,
      address: new WarehouseAddress
      (
        address: Address.Parse(address),
        coordinates: new Coordinates
        (
          latitude: new Latitude(latitude),
          longitude: new Longitude(longitude)
        )
      ),
      contact: new WarehouseContact
      (
        phones: [Phone.Parse(phone)],
        emails: [Email.Parse(email)]
      ),
      merchantId
    );

    _context1.Add(merchantToSave);
    _context1.Add(warehouseToSave);

    // Act
    await _context1.SaveChangesAsync(TestContext.CancellationToken);

    // Assert
    Warehouse? warehouseInDb = await _context2.Set<Warehouse>()
                                              .AsNoTracking()
                                              .SingleOrDefaultAsync(TestContext.CancellationToken);

    Assert.IsNotNull(warehouseInDb);
    Assert.AreEqual(warehouseId, warehouseInDb.Id);
    Assert.AreEqual(merchantId, warehouseInDb.MerchantId);

    Assert.IsNotNull(warehouseInDb.Address);
    Assert.AreEqual(address, warehouseInDb.Address.Address);
    Assert.AreEqual(new Coordinates((Latitude)latitude, (Longitude)longitude), warehouseInDb.Address.Coordinates);

    Assert.IsNotNull(warehouseInDb.Contact);
    Assert.IsNotNull(warehouseInDb.Contact.Phones);
    Assert.HasCount(1, warehouseInDb.Contact.Phones);
    Assert.AreEqual(phone, warehouseInDb.Contact.Phones[0]);

    Assert.IsNotNull(warehouseInDb.Contact);
    Assert.IsNotNull(warehouseInDb.Contact.Emails);
    Assert.HasCount(1, warehouseInDb.Contact.Emails);
    Assert.AreEqual(email, warehouseInDb.Contact.Emails[0]);
  }
}
