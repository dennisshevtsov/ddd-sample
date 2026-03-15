using DddSample.Domain;
using DddSample.Domain.Merchants;

namespace DddSample.Infrastructure;

internal sealed class MerchantRepository : IMerchantRepository
{
  public Task<Merchant?> GetAsync(MerchantId id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task AddAsync(Merchant merchant, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task CommitAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
