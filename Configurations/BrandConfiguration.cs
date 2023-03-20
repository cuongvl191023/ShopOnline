using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Brand");

            _ = entity.HasIndex(e => e.Code).IsUnique();

            _ = entity.Property(e => e.BrandLogo).HasMaxLength(100);
            _ = entity.Property(e => e.BrandName).HasMaxLength(50);
            _ = entity.Property(e => e.BrandNation).HasMaxLength(50);
            _ = entity.Property(e => e.BrandWebsite).HasMaxLength(50);
            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
        }
    }
}