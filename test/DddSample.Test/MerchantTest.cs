using DddSample.Domain;
using DddSample.Domain.Merchants;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Test;

[TestClass]
[TestCategory("Integration")]
public sealed class MerchantTest
{
#pragma warning disable CS8618
  private IServiceScope _scope;
  private DbContext _context1;
  private DbContext _context2;
#pragma warning restore CS8618

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
  [Timeout(2000, CooperativeCancellation = true)]
  public async Task SaveChangesAsync_NewMerchant_MerchantSaved()
  {
    // Arrange
    MerchantId merchantId = MerchantId.New();
    string merchantName = "test merchant name";
    Merchant merchantToSave = new
    (
      id: merchantId,
      name: merchantName
    );
    _context1.Add(merchantToSave);

    // Act
    await _context1.SaveChangesAsync(TestContext.CancellationToken);

    // Assert
    Merchant? merchantInDb = await _context2.Set<Merchant>()
                                            .AsNoTracking()
                                            .SingleOrDefaultAsync(TestContext.CancellationToken);
    Assert.IsNotNull(merchantInDb);
    Assert.AreEqual(merchantId, merchantInDb.Id);
    Assert.AreEqual(merchantName, merchantInDb.Name);
  }
}
