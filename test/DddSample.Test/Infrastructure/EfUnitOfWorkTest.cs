using DddSample.Domain;
using DddSample.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DddSample.Test.Infrastructure;

[TestClass]
public sealed class EfUnitOfWorkTest
{
  private EfUnitOfWork _uow;

  [TestInitialize]
  public void Initialize()
  {
    DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>().UseInMemoryDatabase("test-db").Options;
    _uow = new EfUnitOfWork(new TestDbContext(options));
  }

  [TestMethod]
  public async Task CommitAsync_1AggregateAdded_ExceptionNotThrown()
  {
    // Arrage
    _uow.Add(new TestAggregate1());

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await act();
  }

  [TestMethod]
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

  [TestMethod]
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
    await Assert.ThrowsExactlyAsync<DomainException>(act);
  }

  [TestMethod]
  public async Task CommitAsync_1AggregateRemoved_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Remove(entity1);

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await act();
  }

  [TestMethod]
  public async Task CommitAsync_2SameAggregateRemoved_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Remove(entity1);

    TestAggregate1 entity2 = new();
    _uow.Remove(entity2);

    // Act
    Task act() => _uow.CommitAsync();

    // Assert
    await act();
  }

  [TestMethod]
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
    await Assert.ThrowsExactlyAsync<DomainException>(act);
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
