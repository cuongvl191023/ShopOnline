using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ShopOnline.Models;

public partial class ShopOnlineContext : IdentityDbContext<AppUser>
{
    public ShopOnlineContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<AppUser>? AppUsers { get; set; }

    public virtual DbSet<Brand>? Brands { get; set; }

    public virtual DbSet<Category>? Categories { get; set; }

    public virtual DbSet<CategoryProduct>? CategoryProducts { get; set; }

    public virtual DbSet<Color>? Colors { get; set; }

    public virtual DbSet<Image>? Images { get; set; }

    public virtual DbSet<Material>? Materials { get; set; }

    public virtual DbSet<Product>? Products { get; set; }

    public virtual DbSet<Shoe>? Shoes { get; set; }

    public virtual DbSet<Size>? Sizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.
                ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        _ = modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(p => new { p.LoginProvider, p.ProviderKey });

        _ = modelBuilder.Entity<IdentityUserRole<string>>()
                    .HasKey(p => new { p.UserId, p.RoleId });

        _ = modelBuilder.Entity<IdentityUserToken<string>>()
        .HasKey(p => new { p.UserId, p.LoginProvider, p.Name });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
