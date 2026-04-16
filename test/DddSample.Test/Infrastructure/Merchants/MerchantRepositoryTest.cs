using DddSample.Domain.Merchants;
using DddSample.Infrastructure.Test;
using DddSample.Test;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Merchants.Test;

[TestClass]
[TestCategory("Integration")]
public sealed class MerchantRepositoryTest
{
  private IServiceScope _scope;
  private DbContext _context;
  private IMerchantRepository _merchantRepository;

  private MerchantBuilder _merchantBuilder;

  public TestContext TestContext { get; set; }

  [TestInitialize]
  public async Task InitializeAsync()
  {
    DddSampleWebApplicationFactory factory = new();

    _scope = factory.Services.CreateScope();
    _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    _merchantRepository = _scope.ServiceProvider.GetRequiredService<IMerchantRepository>();

    await _context.Database.EnsureCreatedAsync();

    _merchantBuilder = MerchantBuilder.Default();
  }

  [TestCleanup]
  public async Task CleanupAsync()
  {
    try
    {
      await _context.Set<Merchant>().ExecuteDeleteAsync();
    }
    finally
    {
      _scope?.Dispose();
    }
  }

  [TestMethod(DisplayName = "When a new instance of class Merchant added, a new record is saved to the DB")]
  [Timeout(5000, CooperativeCancellation = true)]
  public async Task CommitAsync_NewMerchant_MerchantSaved()
  {
    // Arrange
    Merchant merchantToSave = _merchantBuilder.Build();
    _merchantRepository.Add(merchantToSave);

    // Act
    await _merchantRepository.CommitAsync(TestContext.CancellationToken);

    // Assert
    Merchant expected = _merchantBuilder.Build();
    Merchant? actual = await _context.Set<Merchant>()
                                     .AsNoTracking()
                                     .SingleOrDefaultAsync(TestContext.CancellationToken);

    Assert.IsNotNull(actual);
    Assert.AreEqual(expected.Id, actual.Id);
    Assert.AreEqual(expected.Name, actual.Name);
  }
}
