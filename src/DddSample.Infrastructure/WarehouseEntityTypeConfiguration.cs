using DddSample.Domain;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddSample.Infrastructure;

internal sealed class WarehouseEntityTypeConfiguration : IEntityTypeConfiguration<Warehouse>
{
  public void Configure(EntityTypeBuilder<Warehouse> builder)
  {
    builder.ToTable("warehouse");
    builder.HasKey(entity => entity.Id);

    builder.Property(entity => entity.Id)
           .HasColumnName("id")
           .IsRequired()
           .HasConversion(id => id.ToString(), id => WarehouseId.Parce(id));
    builder.Property(entity => entity.Address)
           .HasColumnName("address")
           .HasColumnType("jsonb");
    builder.Property(entity => entity.Contact)
           .HasColumnName("contact")
           .HasColumnType("jsonb")
           .IsRequired();
    builder.Property(entity => entity.MerchantId)
           .HasColumnName("merchantId")
           .IsRequired();

    builder.HasOne(typeof(Merchant))
           .WithMany()
           .HasForeignKey(nameof(Warehouse.MerchantId))
           .HasPrincipalKey(nameof(Merchant.Id))
           .HasConstraintName("fk_warehouse_merchant");
  }
}
