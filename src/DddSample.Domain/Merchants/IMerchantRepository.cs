namespace DddSample.Domain.Merchants;

public interface IMerchantRepository
{
  public Task<Merchant?> GetAsync(MerchantId id, CancellationToken cancellationToken = default);
  public void Add(Merchant merchant);
  public void Delete(Merchant merchant);
  public Task CommitAsync(CancellationToken cancellationToken = default);
}
