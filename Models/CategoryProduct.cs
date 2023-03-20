namespace ShopOnline.Models;

public partial class CategoryProduct
{
    public Guid IdCategory { get; set; } = Guid.NewGuid();

    public Guid IdProduct { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
