using DddSample.Domain;
using System.Text.Json;

namespace DddSample.Infrastructure.UnitTest;

public sealed class EmailJsonConverterTest
{
  private readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true,
    IndentCharacter = ' ',
    IndentSize = 2,

    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters =
    {
      new EmailJsonConverter(),
    },
  };

  [Fact(DisplayName = "When an object of type Email is serialized, a string is expected")]
  public void Serialize_Email_CorrectJsonReturned()
  {
    // Arrange
    Email email = Email.Parse("test@test");

    // Act
    string actual = JsonSerializer.Serialize(email, _jsonSerializerOptions);

    // Assert
    string expected = "\"test@test\"";
    Assert.Equal(expected, actual);
  }
}
