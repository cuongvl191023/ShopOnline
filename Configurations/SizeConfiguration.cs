using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Models;

namespace ShopOnline.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> entity)
        {
            _ = entity.HasKey(e => e.Id);

            _ = entity.ToTable("Size");

            _ = entity.HasIndex(e => e.Code).IsUnique();

            _ = entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            _ = entity.Property(e => e.Size1).HasColumnName("Size");
            _ = entity.Property(e => e.SizeName).HasMaxLength(50);
        }
    }
}