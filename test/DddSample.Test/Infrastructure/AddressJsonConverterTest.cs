using DddSample.Domain;
using DddSample.Infrastructure;
using System.Text.Json;

namespace DddSample.Test.Infrastructure;

[TestClass]
public sealed class AddressJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new AddressJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type Address is serialized a string is expected")]
  public void Serialize_Address_CorrectJsonReturned()
  {
    // Arrange
    Address address = Address.Parse("test address");

    // Act
    string actual = JsonSerializer.Serialize(address, _jsonSerializerOptions);

    // Assert
    string expected = "\"test address\"";
    Assert.AreEqual(expected, actual);
  }
}
