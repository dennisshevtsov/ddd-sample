using DddSample.Domain;
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

  public TestContext TestContext { get; set; }

  [TestInitialize]
  public async Task InitializeAsync()
  {
    DddSampleWebApplicationFactory factory = new();

    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _deliveryPointRepository = _scope.ServiceProvider.GetRequiredService<IDeliveryPointRepository>();

    await _context.Database.EnsureCreatedAsync();

    _merchantBuilder = new MerchantBuilder();
    Merchant merchant = _merchantBuilder.Id(MerchantId.New())
                                        .Name("test merchant name")
                                        .Build();
    _context.Add(merchant);

    _warehouseBuilder = new WarehouseBuilder();
    Warehouse warehouse = _warehouseBuilder.Id(WarehouseId.New())
                                           .Address(Address.Parse("test warehouse address"))
                                           .Coordinates(new Coordinates(new Latitude(1D), new Longitude(2D)))
                                           .Email(Email.Parse("test@test"))
                                           .Phone(Phone.Parse("375331234567"))
                                           .MerchantId(merchant.Id)
                                           .Build();
    _context.Add(warehouse);

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

  [TestMethod]
  [Timeout(5000, CooperativeCancellation = true)]
  public async Task CommitAsync_NewDeliveryPoint_DeliveryPointSaved()
  {
    // Arrange
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
      warehouseId: _warehouseBuilder.WarehouseId
    );

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
