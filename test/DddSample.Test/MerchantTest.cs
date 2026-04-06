using DddSample.Domain;
using DddSample.Domain.Merchants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Testing.Platform.Services;

namespace DddSample.Test;

[TestClass]
public sealed class MerchantTest
{
  private readonly DbContext _context1;
  private readonly DbContext _context2;

  public MerchantTest()
  {
    WebApplicationFactory<Program> factory = new();
    _context1 = factory.Services.GetRequiredService<DbContext>();
    _context2 = factory.Services.GetRequiredService<DbContext>();
  }

  [TestInitialize]
  public async Task InitializeAsync()
  {
    await _context1.Database.EnsureCreatedAsync();
  }

  [TestCleanup]
  public async Task CleanupAsync()
  {
    await _context1.Database.EnsureDeletedAsync();
    _context1.Dispose();
    _context2.Dispose();
  }

  [TestMethod]
  public async Task SaveChangesAsync_NewMerchant_MerchantSaved(TestContext testContext)
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
    await _context1.SaveChangesAsync(testContext.CancellationToken);

    // Assert
    Merchant merchantInDb = await _context2.Set<Merchant>()
                                           .AsNoTracking()
                                           .SingleAsync(testContext.CancellationToken);
    Assert.AreEqual(merchantId, merchantInDb.Id);
    Assert.AreEqual(merchantName, merchantInDb.Name);
  }
}
