using DddSample.Domain;
using DddSample.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DddSample.Test.Infrastructure;

[TestClass]
public sealed class EfUnitOfWorkTest
{
  private EfUnitOfWork _uow;

  [TestInitialize]
  public void Initialize()
  {
    _uow = new EfUnitOfWork(Mock.Of<DbContext>());
  }

  [TestMethod]
  public void Add_1Aggregate_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();

    // Act
    Action act = () => _uow.Add(entity1);

    // Assert
    act();
  }

  [TestMethod]
  public void Add_2SameAggregate_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Add(entity1);

    TestAggregate1 entity2 = new();

    // Act
    Action act = () => _uow.Add(entity2);

    // Assert
    act();
  }

  [TestMethod]
  public void Add_2DifferentAggregate_ExceptionThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Add(entity1);

    TestAggregate2 entity2 = new();

    // Act
    Action act = () => _uow.Add(entity2);

    // Assert
    Assert.ThrowsExactly<DomainException>(act);
  }

  [TestMethod]
  public void Remove_1Aggregate_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();

    // Act
    Action act = () => _uow.Remove(entity1);

    // Assert
    act();
  }

  [TestMethod]
  public void Remove_2SameAggregate_ExceptionNotThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Remove(entity1);

    TestAggregate1 entity2 = new();

    // Act
    Action act = () => _uow.Remove(entity2);

    // Assert
    act();
  }

  [TestMethod]
  public void Remove_2DifferentAggregate_ExceptionThrown()
  {
    // Arrage
    TestAggregate1 entity1 = new();
    _uow.Remove(entity1);

    TestAggregate2 entity2 = new();

    // Act
    Action act = () => _uow.Remove(entity2);

    // Assert
    Assert.ThrowsExactly<DomainException>(act);
  }

  private sealed class TestAggregate1 { }
  private sealed class TestAggregate2 { }
}
