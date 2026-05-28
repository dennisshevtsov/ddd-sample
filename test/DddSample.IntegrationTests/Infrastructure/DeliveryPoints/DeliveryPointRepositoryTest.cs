using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure.IntegrationTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddSample.Infrastructure.DeliveryPoints.IntegrationTests;

public sealed class DeliveryPointRepositoryTest : IClassFixture<DddSampleWebApplicationFactory>, IAsyncLifetime
{
  private readonly IServiceScope _scope;
  private readonly DbContext _context;
  private readonly IUnitOfWork _uow;
  private readonly IDeliveryPointRepository _deliveryPointRepository;

  private readonly DeliveryPointBuilder _deliveryPointBuilder;

  public DeliveryPointRepositoryTest(DddSampleWebApplicationFactory factory)
  {
    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _uow = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    _deliveryPointRepository = _scope.ServiceProvider.GetRequiredService<IDeliveryPointRepository>();

    _deliveryPointBuilder = DeliveryPointBuilder.Default();
  }

  public async ValueTask InitializeAsync()
  {
    await _context.Database.EnsureCreatedAsync();

    Warehouse warehouse = WarehouseBuilder.Default()
                                          .Build();
    _context.Add(warehouse);
    await _context.SaveChangesAsync();

    _deliveryPointBuilder.WarehouseId(warehouse.Id);
  }

  public async ValueTask DisposeAsync()
  {
    try
    {
      await _context.Set<DeliveryPoint>().ExecuteDeleteAsync();
      await _context.Set<Warehouse>().ExecuteDeleteAsync();
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
    DeliveryPoint deliveryPointToSave = _deliveryPointBuilder.Build();
    _deliveryPointRepository.Add(deliveryPointToSave);

    // Act
    await _uow.CommitAsync(TestContext.Current.CancellationToken);

    // Assert
    DeliveryPoint expected = _deliveryPointBuilder.Build();
    DeliveryPoint? actual = await _context.Set<DeliveryPoint>()
                                          .AsNoTracking()
                                          .Where(deliveryPoint => deliveryPoint.Id == deliveryPointToSave.Id)
                                          .SingleOrDefaultAsync(TestContext.Current.CancellationToken);

    Assert.NotNull(actual);
    Assert.Equal(expected.Id, actual.Id);

    Assert.NotNull(actual.Address);
    Assert.Equal(expected.Address.Address, actual.Address.Address);
    Assert.Equal(expected.Address.Coordinates, actual.Address.Coordinates);

    Assert.NotNull(actual.OpeningHours);
    Assert.Equal(expected.OpeningHours.WorksOnHolidays, actual.OpeningHours.WorksOnHolidays);
    Assert.Equal(expected.OpeningHours.Mon, actual.OpeningHours.Mon);
    Assert.Equal(expected.OpeningHours.Tue, actual.OpeningHours.Tue);
    Assert.Equal(expected.OpeningHours.Wed, actual.OpeningHours.Wed);
    Assert.Equal(expected.OpeningHours.Thu, actual.OpeningHours.Thu);
    Assert.Equal(expected.OpeningHours.Fri, actual.OpeningHours.Fri);
    Assert.Equal(expected.OpeningHours.Sat, actual.OpeningHours.Sat);
    Assert.Equal(expected.OpeningHours.Sun, actual.OpeningHours.Sun);

    Assert.Equal(expected.WarehouseId, actual.WarehouseId);
  }
}
