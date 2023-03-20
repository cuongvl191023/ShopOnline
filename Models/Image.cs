namespace ShopOnline.Models;

public partial class Image
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public string Image1 { get; set; } = null!;

    public Guid IdProduct { get; set; }

    public int Status { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;
}
