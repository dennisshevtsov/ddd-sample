using DddSample.Domain;
using System.Text.Json;

namespace DddSample.Infrastructure.UnitTest;

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

  [Fact(DisplayName = "When an object of type Address is serialized, a string is expected")]
  public void Serialize_Address_CorrectJsonReturned()
  {
    // Arrange
    Address address = Address.Parse("test address");

    // Act
    string actual = JsonSerializer.Serialize(address, _jsonSerializerOptions);

    // Assert
    string expected = "\"test address\"";
    Assert.Equal(expected, actual);
  }
}
