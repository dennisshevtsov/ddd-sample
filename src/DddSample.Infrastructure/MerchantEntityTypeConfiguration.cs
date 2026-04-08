using DddSample.Domain;
using DddSample.Domain.Merchants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddSample.Infrastructure;

internal sealed class MerchantEntityTypeConfiguration : IEntityTypeConfiguration<Merchant>
{
  public void Configure(EntityTypeBuilder<Merchant> builder)
  {
    builder.ToTable("merchant");
    builder.HasKey(entity => entity.Id);

    builder.Property(entity => entity.Id)
           .HasColumnName("id")
           .IsRequired()
           .HasConversion(id => id.ToString(), id => MerchantId.Parce(id));
    builder.Property(entity => entity.Name).HasColumnName("name");
    builder.Property(entity => entity.Deleted).HasColumnName("deleted").IsRequired();
  }
}
