using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddSample.Infrastructure.IntegrationTest.Merchants;

public sealed class MerchantRepositoryTest : IClassFixture<DddSampleWebApplicationFactory>, IAsyncLifetime
{
  private readonly IServiceScope _scope;
  private readonly DbContext _context;
  private readonly IUnitOfWork _uow;
  private readonly IMerchantRepository _merchantRepository;

  private readonly MerchantBuilder _merchantBuilder;

  public MerchantRepositoryTest(DddSampleWebApplicationFactory factory)
  {
    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _uow = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    _merchantRepository = _scope.ServiceProvider.GetRequiredService<IMerchantRepository>();

    _merchantBuilder = MerchantBuilder.Default();
  }

  public async ValueTask InitializeAsync()
  {
    await _context.Database.EnsureCreatedAsync();

    Warehouse warehouse = WarehouseBuilder.Default()
                                          .Build();
    _context.Add(warehouse);
    await _context.SaveChangesAsync();

    DeliveryPoint deliveryPoint = DeliveryPointBuilder.Default()
                                                      .WarehouseId(warehouse.Id)
                                                      .Build();
    _context.Add(deliveryPoint);
    await _context.SaveChangesAsync();

    _merchantBuilder.DeliveryPointId(deliveryPoint.Id);
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

  [Fact(DisplayName = "When a new instance of class Merchant added, a new record is saved to the DB")]
  public async Task CommitAsync_NewMerchant_MerchantSaved()
  {
    // Arrange
    Merchant merchantToSave = _merchantBuilder.Build();
    _merchantRepository.Add(merchantToSave);

    // Act
    await _uow.CommitAsync(TestContext.Current.CancellationToken);

    // Assert
    Merchant expected = _merchantBuilder.Build();
    Merchant? actual = await _context.Set<Merchant>()
                                     .AsNoTracking()
                                     .Where(merchant => merchant.Id == expected.Id)
                                     .SingleOrDefaultAsync(TestContext.Current.CancellationToken);

    Assert.NotNull(actual);
    Assert.Equal(expected.Id, actual.Id);
    Assert.Equal(expected.Name, actual.Name);
    Assert.Equal(expected.DeliveryPointId, actual.DeliveryPointId);
  }
}
