using DddSample.Domain;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure;

internal sealed class EfUnitOfWork(DbContext context) : IUnitOfWork
{
  public async Task CommitAsync(CancellationToken cancellationToken = default)
  {
    await context.SaveChangesAsync(cancellationToken);
    Untrack();
  }

  internal IQueryable<T> AsQueryable<T>() where T : class
  {
    return context.Set<T>();
  }

  internal void Add<T>(T entity) where T : class
  {
    Track<T>();
    context.Set<T>().Add(entity);
  }

  internal void Remove<T>(T entity) where T : class
  {
    Track<T>();
    context.Set<T>().Remove(entity);
  }

  private Type? _tracked;
  private void Track<T>() where T : class
  {
    if (_tracked is null)
    {
      _tracked = typeof(T);
      return;
    }

    if (_tracked != typeof(T))
    {
      throw new DomainException("Impossible to modify 2+ aggregate in 1 transation");
    }
  }
  private void Untrack() => _tracked = null;
}
