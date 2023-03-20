namespace ShopOnline.Models;

public partial class Material
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public string MaterialName { get; set; } = null!;

    public int MaterialReliability { get; set; }

    public int MaterialLevel { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
