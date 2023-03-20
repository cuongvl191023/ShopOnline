using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Category");

            _ = entity.HasIndex(e => e.Code).IsUnique();

            _ = entity.Property(e => e.CategoryName).HasMaxLength(50);
            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);

            _ = entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.InverseIdCategoryNavigation)
                .HasForeignKey(d => d.IdCategory);
        }
    }
}