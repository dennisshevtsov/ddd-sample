using DddSample.Domain;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Infrastructure.UnitTest;

public sealed class EfUnitOfWorkTest
{
  private readonly DbContext _context;
  private readonly EfUnitOfWork _uow;

  public EfUnitOfWorkTest()
  {
    DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>().UseInMemoryDatabase("test-db").Options;
    _context = new TestDbContext(options);
    _uow = new EfUnitOfWork(_context);
  }

  [Fact]
  public async Task CommitAsync_1AggregateAdded_ExceptionNotThrown()
  {
    // Arrage
    _uow.Add(new TestAggregate1());

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await act();
  }

  [Fact]
  public async Task CommitAsync_2SameAggregateAdded_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Add(entity1);

    TestAggregate1 entity2 = new();
    _uow.Add(entity2);

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await act();
  }

  [Fact]
  public async Task CommitAsync_2DifferentAggregateAdded_ExceptionThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Add(entity1);

    TestAggregate2 entity2 = new();
    _uow.Add(entity2);

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await Assert.ThrowsAsync<DomainException>(act);
  }

  [Fact]
  public async Task CommitAsync_1AggregateRemoved_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _context.Add(entity1);
    _uow.Remove(entity1);

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await act();
  }

  [Fact]
  public async Task CommitAsync_2SameAggregateRemoved_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    TestAggregate1 entity2 = new();

    _context.Add(entity1);
    _context.Add(entity2);

    _uow.Remove(entity1);
    _uow.Remove(entity2);

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await act();
  }

  [Fact]
  public async Task CommitAsync_2DifferentAggregateRemoved_ExceptionThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Remove(entity1);

    TestAggregate2 entity2 = new();
    _uow.Remove(entity2);

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await Assert.ThrowsAsync<DomainException>(act);
  }

  private sealed class TestDbContext(DbContextOptions options) : DbContext(options)
  {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<TestAggregate1>().HasKey(entity => entity.Id);
      modelBuilder.Entity<TestAggregate2>().HasKey(entity => entity.Id);
    }
  }

  private sealed class TestAggregate1 : IAggregate
  {
    public Guid Id { get; set; }
  }

  private sealed class TestAggregate2 : IAggregate
  {
    public Guid Id { get; set; }
  }
}
