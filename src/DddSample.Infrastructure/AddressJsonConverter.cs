using DddSample.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddSample.Infrastructure;

internal sealed class AddressJsonConverter : JsonConverter<Address>
{
  public override Address Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? addressLine = reader.GetString();
    if (string.IsNullOrWhiteSpace(addressLine))
    {
      return Address.None;
    }

    return Address.Parse(addressLine);
  }

  public override void Write(Utf8JsonWriter writer, Address value, JsonSerializerOptions options)
  {
    writer.WriteStringValue(value.ToString());
  }
}
