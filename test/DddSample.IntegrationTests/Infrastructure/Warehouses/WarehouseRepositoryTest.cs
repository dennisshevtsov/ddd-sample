using DddSample.Domain;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddSample.Infrastructure.IntegrationTest.Warehouses;

public sealed class WarehouseRepositoryTest : IClassFixture<DddSampleWebApplicationFactory>, IAsyncLifetime
{
  private readonly IServiceScope _scope;
  private readonly DbContext _context;
  private readonly IUnitOfWork _uow;
  private readonly IWarehouseRepository _warehouseRepository;

  private readonly WarehouseBuilder _warehouseBuilder;

  public WarehouseRepositoryTest(DddSampleWebApplicationFactory factory)
  {
    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _uow = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    _warehouseRepository = _scope.ServiceProvider.GetRequiredService<IWarehouseRepository>();

    _warehouseBuilder = WarehouseBuilder.Default();
  }

  public async ValueTask InitializeAsync()
  {
    await _context.Database.EnsureCreatedAsync();
  }

  public async ValueTask DisposeAsync()
  {
    try
    {
      await _context.Set<Warehouse>().ExecuteDeleteAsync();
    }
    finally
    {
      _scope?.Dispose();
    }
  }

  [Fact(DisplayName = "When a new instance of class Warehouse added, a new record is saved to the DB")]
  public async Task CommitAsync_NewWarehouse_WarehouseSaved()
  {
    // Arrange
    Warehouse warehouseToSave = _warehouseBuilder.Build();
    _warehouseRepository.Add(warehouseToSave);

    // Act
    await _uow.CommitAsync(TestContext.Current.CancellationToken);

    // Assert
    Warehouse expected = _warehouseBuilder.Build();
    Warehouse? actual = await _context.Set<Warehouse>()
                                      .AsNoTracking()
                                      .Where(warehouse => warehouse.Id == expected.Id)
                                      .SingleOrDefaultAsync(TestContext.Current.CancellationToken);

    Assert.NotNull(actual);
    Assert.Equal(expected.Id, actual.Id);

    Assert.NotNull(actual.Address);
    Assert.Equal(expected.Address.Address, actual.Address.Address);
    Assert.Equal(expected.Address.Coordinates, actual.Address.Coordinates);

    Assert.NotNull(actual.Contact);
    Assert.NotNull(actual.Contact.Phones);
    Assert.Single(actual.Contact.Phones);
    Assert.Equal(expected.Contact.Phones[0], actual.Contact.Phones[0]);

    Assert.NotNull(actual.Contact);
    Assert.NotNull(actual.Contact.Emails);
    Assert.Single(actual.Contact.Emails);
    Assert.Equal(expected.Contact.Emails[0], actual.Contact.Emails[0]);
  }
}
