using System.Text.Json;

namespace DddSample.Infrastructure;

internal static class JsonSerializerOptionsExtensions
{
  internal static string ConvertName(this JsonSerializerOptions options, string name)
  {
    if (options.PropertyNamingPolicy is null)
    {
      return name;
    }
    return options.PropertyNamingPolicy.ConvertName(name);
  }
}
