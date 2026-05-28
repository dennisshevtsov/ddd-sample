using DddSample.Infrastructure.IntegrationTests;
using DddSample.Resources.DeliveryPoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddSample.IntegrationTests.Resources.DeliveryPoints;

public class DeliveryPointControllerTests : IClassFixture<DddSampleWebApplicationFactory>, IAsyncLifetime
{
  private readonly AsyncServiceScope _scope;
  private readonly DbContext _context;
  private readonly IDeliveryPointApi _api;

  public DeliveryPointControllerTests(DddSampleWebApplicationFactory factory)
  {
    _scope = factory.Services.CreateAsyncScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _api = _scope.ServiceProvider.GetRequiredService<IDeliveryPointApi>();
  }

  public async ValueTask InitializeAsync()
  {
    await _context.Database.EnsureCreatedAsync();

    string sqlPath = Path.Combine(AppContext.BaseDirectory, "Resources\\DeliveryPoints\\DeliveryPointControllerTestsInitialize.sql");
    string sql = await File.ReadAllTextAsync(sqlPath);
    await _context.Database.ExecuteSqlRawAsync(sql);
  }

  public async ValueTask DisposeAsync()
  {
    await _context.Database.ExecuteSqlRawAsync(
      """
      DELETE FROM delivery_point;
      DELETE FROM warehouse;
      """
    );
    await _scope.DisposeAsync();
  }

  [Fact]
  public async Task Get_ExistingMerchant_MerchantReturned()
  {
    // Arrange
    string warehouseId = "11111111-1111-1111-1111-111111111111";
    string id = "22222222-2222-2222-2222-222222222222";

    // Act
    DeliveryPointResource actual = await _api.Get(warehouseId, id);

    // Assert
    Assert.NotNull(actual);
  }
}
