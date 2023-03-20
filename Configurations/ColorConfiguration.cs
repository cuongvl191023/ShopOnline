using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Color");

            _ = entity.HasIndex(e => e.Code).IsUnique();

            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            _ = entity.Property(e => e.ColorCode).HasMaxLength(50);
            _ = entity.Property(e => e.ColorName).HasMaxLength(50);
        }
    }
}