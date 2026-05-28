using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using DddSample.Infrastructure.IntegrationTests;
using DddSample.Resources.Merchants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddSample.IntegrationTests.Resources.Merchants;

public sealed class MerchantControllerTest : IClassFixture<DddSampleWebApplicationFactory>, IAsyncLifetime
{
  private readonly MerchantBuilder _merchantBuilder;

  private readonly AsyncServiceScope _scope;
  private readonly DbContext _context;
  private readonly IMerchantApi _api;

  public MerchantControllerTest(DddSampleWebApplicationFactory factory)
  {
    _merchantBuilder = MerchantBuilder.Default();

    _scope = factory.Services.CreateAsyncScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _api = _scope.ServiceProvider.GetRequiredService<IMerchantApi>();
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

    Merchant merchant = _merchantBuilder.DeliveryPointId(deliveryPoint.Id)
                                        .Build();
    _context.Add(merchant);
    await _context.SaveChangesAsync();
  }

  public async ValueTask DisposeAsync()
  {
    await _context.Set<Merchant>().ExecuteDeleteAsync();
    await _scope.DisposeAsync();
  }

  [Fact]
  public async Task Get_ExistingMerchant_MerchantReturned()
  {
    // Arrange
    string id = _merchantBuilder.MerchantId.ToString();

    // Act
    MerchantResource actual = await _api.Get(id);

    // Assert
    Merchant merchant = _merchantBuilder.Build();

    Assert.NotNull(actual);
    Assert.Equal(merchant.Id.ToString(), actual.Id);
    Assert.Equal(merchant.Name, actual.Name);
    Assert.Equal(merchant.DeliveryPointId.ToString(), actual.DeliveryPointId);
    Assert.Equal(merchant.Deleted, actual.Deleted);
  }
}
