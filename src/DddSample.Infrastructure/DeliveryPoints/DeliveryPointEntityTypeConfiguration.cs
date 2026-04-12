using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddSample.Infrastructure.DeliveryPoints;

internal sealed class DeliveryPointEntityTypeConfiguration : IEntityTypeConfiguration<DeliveryPoint>
{
  public void Configure(EntityTypeBuilder<DeliveryPoint> builder)
  {
    builder.ToTable("delivery_point");
    builder.HasKey(entity => entity.Id)
           .HasName("pk_delivery_point");

    builder.Property(entity => entity.Id)
           .HasColumnName("id")
           .IsRequired()
           .HasConversion(id => id.ToString(), id => DeliveryPointId.Parce(id));
    builder.Property(entity => entity.Address)
           .HasColumnName("address")
           .HasColumnType("jsonb");
    builder.Property(entity => entity.OpeningHours)
           .HasColumnName("opening_hours")
           .HasColumnType("jsonb")
           .IsRequired();
    builder.Property(entity => entity.WarehouseId)
           .HasColumnName("warehouse_id")
           .IsRequired();

    builder.HasOne(typeof(Warehouse))
           .WithMany()
           .HasForeignKey(nameof(DeliveryPoint.WarehouseId))
           .HasPrincipalKey(nameof(Warehouse.Id))
           .HasConstraintName("fk_delivery_point_warehouse");
  }
}
