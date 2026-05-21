using DddSample.Domain;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DddSample.Infrastructure.Warehouses;

internal sealed class WarehouseEntityTypeConfiguration(JsonSerializerOptions options) : IEntityTypeConfiguration<Warehouse>
{
  public void Configure(EntityTypeBuilder<Warehouse> builder)
  {
    builder.ToTable("warehouse");
    builder.HasKey(entity => entity.Id)
           .HasName("pk_warehouse");

    builder.Property(entity => entity.Id)
           .HasColumnName("id")
           .IsRequired()
           .HasConversion(id => id.ToString(), id => WarehouseId.Parce(id));

    builder.Property(entity => entity.Address)
           .HasColumnName("address")
           .IsRequired()
           .IsJsonb(options);

    builder.Property(entity => entity.Contact)
           .HasColumnName("contact")
           .IsRequired()
           .IsJsonb(options);
  }
}
