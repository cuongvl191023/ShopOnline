using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> entity)
        {
            _ = entity
                .HasNoKey()
                .ToTable("Category-Product");

            _ = entity.HasOne(d => d.IdCategoryNavigation).WithMany()
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.Cascade);

            _ = entity.HasOne(d => d.IdProductNavigation).WithMany()
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}