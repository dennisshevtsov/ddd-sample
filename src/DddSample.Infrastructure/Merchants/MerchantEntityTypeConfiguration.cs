using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using DddSample.Domain.Merchants;
using DddSample.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddSample.Infrastructure.Merchants;

internal sealed class MerchantEntityTypeConfiguration : IEntityTypeConfiguration<Merchant>
{
  public void Configure(EntityTypeBuilder<Merchant> builder)
  {
    builder.ToTable("merchant");
    builder.HasKey(entity => entity.Id)
           .HasName("pk_merchant");

    builder.Property(entity => entity.Id)
           .HasColumnName("id")
           .IsRequired()
           .HasConversion(id => id.ToString(), id => MerchantId.Parce(id));
    builder.Property(entity => entity.Name).HasColumnName("name");
    builder.Property(entity => entity.Deleted).HasColumnName("deleted").IsRequired();

    builder.Property(entity => entity.DeliveryPointId)
           .HasColumnName("delivery_point_id")
           .IsRequired();
    builder.HasOne(typeof(DeliveryPoint))
           .WithMany()
           .HasForeignKey(nameof(Merchant.DeliveryPointId))
           .HasPrincipalKey(nameof(DeliveryPoint.Id))
           .HasConstraintName("fk_merchant_delivery_point");
  }
}
