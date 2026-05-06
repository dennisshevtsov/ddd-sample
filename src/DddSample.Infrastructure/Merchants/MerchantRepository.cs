using DddSample.Domain;
using DddSample.Domain.Merchants;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.Merchants;

internal sealed class MerchantRepository(EfUnitOfWork uow) : IMerchantRepository
{
  public async Task<Merchant?> GetAsync(MerchantId id, CancellationToken cancellationToken = default)
  {
    return await uow.AsQueryable<Merchant>()
                    .Where(merchant => merchant.Id == id)
                    .FirstOrDefaultAsync();
  }

  public void Add(Merchant merchant) => uow.Add(merchant);

  public void Remove(Merchant merchant) => uow.Remove(merchant);
}
