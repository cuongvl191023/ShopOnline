namespace ShopOnline.Models;

public partial class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Code { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string? Describe { get; set; }

    public Guid? IdCategory { get; set; }

    public int Status { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual ICollection<Category> InverseIdCategoryNavigation { get; } = new List<Category>();
}
