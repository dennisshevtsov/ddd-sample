namespace DddSample.Domain;

public interface IUnitOfWork
{
  public Task CommitAsync(CancellationToken cancellationToken = default);
}
