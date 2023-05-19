namespace GuitarShop.DAL.Entities;

public class Category
{
    public int Id { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual Category ParentCategory { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
