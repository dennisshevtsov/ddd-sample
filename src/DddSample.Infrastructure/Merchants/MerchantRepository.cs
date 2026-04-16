using DddSample.Domain;
using DddSample.Domain.Merchants;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Merchants;

internal sealed class MerchantRepository(DbContext context) : IMerchantRepository
{
  public async Task<Merchant?> GetAsync(MerchantId id, CancellationToken cancellationToken = default)
  {
    return await context.Set<Merchant>()
                        .Where(merchant => merchant.Id == id)
                        .FirstOrDefaultAsync();
  }

  public void Add(Merchant merchant)
  {
    context.Add(merchant);
  }

  public void Delete(Merchant merchant)
  {
    context.Remove(merchant);
  }

  public Task CommitAsync(CancellationToken cancellationToken = default)
  {
    return context.SaveChangesAsync();
  }
}
