namespace DddSample.Domain.Merchants;

public interface IMerchantRepository
{
  public Task<Merchant?> GetAsync(MerchantId id, CancellationToken cancellationToken = default);
  public Task AddAsync(Merchant merchant, CancellationToken cancellationToken = default);
  public Task DeleteAsync(Merchant merchant, CancellationToken cancellationToken = default);
  public Task CommitAsync(CancellationToken cancellationToken = default);
}
