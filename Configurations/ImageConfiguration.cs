using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Image");

            _ = entity.HasIndex(e => e.Code).IsUnique();

            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            _ = entity.Property(e => e.Image1)
                .HasMaxLength(100)
                .HasColumnName("Image");
            _ = entity.HasOne(d => d.IdProductNavigation).WithOne(p => p.IdImageNavigation)
                .HasForeignKey<Image>(d => d.IdProduct)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}