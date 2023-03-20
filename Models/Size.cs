namespace ShopOnline.Models;

public partial class Size
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public string SizeName { get; set; } = null!;

    public int Size1 { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
