namespace ShopOnline.Models;

public partial class Color
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public string ColorCode { get; set; } = null!;

    public string ColorName { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
