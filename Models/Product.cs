namespace ShopOnline.Models;

public partial class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public Guid IdColor { get; set; }

    public Guid IdBrand { get; set; }

    public Guid IdMaterial { get; set; }

    public Guid IdSize { get; set; }

    public Guid IdShoe { get; set; }

    public double Price { get; set; }

    public string? Describe { get; set; }

    public int Status { get; set; }

    public virtual Brand IdBrandNavigation { get; set; } = null!;

    public virtual Color IdColorNavigation { get; set; } = null!;

    public virtual Material IdMaterialNavigation { get; set; } = null!;

    public virtual Shoe IdShoeNavigation { get; set; } = null!;

    public virtual Size IdSizeNavigation { get; set; } = null!;

    public virtual Image IdImageNavigation { get; set; } = null!;
}
