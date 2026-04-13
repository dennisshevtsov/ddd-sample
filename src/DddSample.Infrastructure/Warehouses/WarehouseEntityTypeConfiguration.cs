using DddSample.Domain;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DddSample.Infrastructure.Warehouses;

internal sealed class WarehouseEntityTypeConfiguration : IEntityTypeConfiguration<Warehouse>
{
  private readonly JsonSerializerOptions _options;

  internal WarehouseEntityTypeConfiguration(JsonSerializerOptions options)
  {
     _options = options;
  }

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
           .HasConversion(address => JsonSerializer.Serialize(address, _options), address => JsonSerializer.Deserialize<WarehouseAddress>(address, _options));

    builder.Property(entity => entity.Contact)
           .HasColumnName("contact")
           .HasColumnType("jsonb")
           .IsRequired()
           .HasConversion(contact => JsonSerializer.Serialize(contact, _options), contact => JsonSerializer.Deserialize<WarehouseContact>(contact, _options));

    builder.Property(entity => entity.MerchantId)
           .HasColumnName("merchant_id")
           .IsRequired();
    builder.HasOne(typeof(Merchant))
           .WithMany()
           .HasForeignKey(nameof(Warehouse.MerchantId))
           .HasPrincipalKey(nameof(Merchant.Id))
           .HasConstraintName("fk_warehouse_merchant");
  }
}
