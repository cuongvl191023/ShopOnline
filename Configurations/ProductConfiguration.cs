using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Product");

            _ = entity.HasIndex(e => e.Code).IsUnique();
            _ = entity.HasIndex(e => new { e.IdBrand, e.IdColor, e.IdMaterial, e.IdShoe, e.IdSize }).IsUnique();

            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.IdBrandNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdBrand)
                .OnDelete(DeleteBehavior.Cascade);

            _ = entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdColor)
                .OnDelete(DeleteBehavior.Cascade);

            _ = entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.Cascade);

            _ = entity.HasOne(d => d.IdShoeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdShoe)
                .OnDelete(DeleteBehavior.Cascade);

            _ = entity.HasOne(d => d.IdSizeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdSize)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}