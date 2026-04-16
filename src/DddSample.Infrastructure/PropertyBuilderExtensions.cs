using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DddSample.Infrastructure;

internal static class PropertyBuilderExtensions
{
  internal static PropertyBuilder<TProperty> IsJsonb<TProperty>(this PropertyBuilder<TProperty> builder, JsonSerializerOptions options)
  {
    builder.HasColumnType("jsonb")
           .HasConversion(value => JsonSerializer.Serialize(value, options), value => JsonSerializer.Deserialize<TProperty>(value, options)!);
    return builder;
  }
}
