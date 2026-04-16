using DddSample.Domain;
using DddSample.Domain.Warehouses;
using System.Text.Json;

namespace DddSample.Infrastructure.Warehouses.Test;

[TestClass]
public sealed class WarehouseContactJsonConverterTest
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
      new WarehouseContactJsonConverter(),
      new PhoneJsonConverter(),
    },
  };

  [TestMethod(DisplayName = "When an object of type WarehouseContact is serialized a snake-case JSON is expected")]
  public void Serialize_WarehouseContact_CorrectJsonReturned()
  {
    // Arrange
    WarehouseContact contact = new
    (
      emails: [Email.Parse("test@test")],
      phones: [Phone.Parse("375331234567")]
    );

    // Act
    string actual = JsonSerializer.Serialize(contact, _jsonSerializerOptions);

    // Assert
    string expected = @"{
  ""emails"": [
    ""test@test""
  ],
  ""phones"": [
    ""375331234567""
  ]
}";
    Assert.AreEqual(expected, actual);
  }
}
