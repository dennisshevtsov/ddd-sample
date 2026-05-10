using DddSample.Domain;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure;

internal sealed class EfUnitOfWork(DbContext context) : IUnitOfWork
{
  public async Task CommitAsync(CancellationToken cancellationToken = default)
  {
    HashSet<Type> changedAggregates = GetChangedAggregates();
    if (changedAggregates.Count > 1)
    {
      throw new DomainException($"Forgidden to modify 2+ aggregate in 1 transation. Aggregates: {string.Join(", ", changedAggregates)}");
    }
    await context.SaveChangesAsync(cancellationToken);
  }

  internal IQueryable<T> AsQueryable<T>() where T : class
  {
    return context.Set<T>();
  }

  internal void Add<T>(T entity) where T : class, IAggregate
  {
    context.Set<T>().Add(entity);
  }

  internal void Remove<T>(T entity) where T : class, IAggregate
  {
    context.Set<T>().Remove(entity);
  }

  private HashSet<Type> GetChangedAggregates() =>
    context.ChangeTracker
           .Entries()
           .Where(entry => entry.State != EntityState.Unchanged)
           .Where(entry => typeof(IAggregate).IsAssignableFrom(entry.Entity.GetType()))
           .Select(entry => entry.Entity.GetType())
           .ToHashSet();
}
