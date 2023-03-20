namespace ShopOnline.Models;

public partial class Brand
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public string BrandName { get; set; } = null!;

    public string BrandLogo { get; set; } = null!;

    public string BrandNation { get; set; } = null!;

    public string BrandWebsite { get; set; } = null!;

    public string? BrandDescribe { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
