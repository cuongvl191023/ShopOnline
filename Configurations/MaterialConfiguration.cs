using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Material");

            _ = entity.HasIndex(e => e.Code).IsUnique();

            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            _ = entity.Property(e => e.MaterialName).HasMaxLength(50);
        }
    }
}