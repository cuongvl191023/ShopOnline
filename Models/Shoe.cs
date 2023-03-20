namespace ShopOnline.Models;

public partial class Shoe
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public string ShoeName { get; set; } = null!;

    public string? ShoeDescribe { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
