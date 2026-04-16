using DddSample.Domain;
using DddSample.Infrastructure;
using System.Text.Json;

namespace DddSample.Test.Infrastructure;

[TestClass]
public sealed class PhoneJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true,
    IndentCharacter = ' ',
    IndentSize = 2,

    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new PhoneJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type Phone is serialized, a string is expected")]
  public void Serialize_Phone_CorrectJsonReturned()
  {
    // Arrange
    Phone email = Phone.Parse("375331234567");

    // Act
    string actual = JsonSerializer.Serialize(email, _jsonSerializerOptions);

    // Assert
    string expected = "\"375331234567\"";
    Assert.AreEqual(expected, actual);
  }
}
