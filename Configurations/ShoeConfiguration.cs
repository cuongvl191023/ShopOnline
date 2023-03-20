using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class ShoeConfiguration : IEntityTypeConfiguration<Shoe>
    {
        public void Configure(EntityTypeBuilder<Shoe> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Shoe");

            _ = entity.HasIndex(e => e.Code).IsUnique();

            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            _ = entity.Property(e => e.ShoeName).HasMaxLength(50);
        }
    }
}