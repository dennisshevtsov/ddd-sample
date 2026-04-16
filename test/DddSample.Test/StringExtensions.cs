using Newtonsoft.Json.Linq;

namespace DddSample.Test;

internal static class StringExtensions
{
  internal static bool JsonEquals(this string left, string right)
  {
    JObject obj1 = JObject.Parse(left);
    JObject obj2 = JObject.Parse(right);
    return JObject.DeepEquals(obj1, obj2);
  }
}
