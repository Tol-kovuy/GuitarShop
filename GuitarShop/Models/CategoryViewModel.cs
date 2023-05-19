using GuitarShop.DAL.Entities;
using System.ComponentModel;

namespace GuitarShop.Models;

public class CategoryViewModel
{
    public int Id { get; set; }

    [DisplayName("Category Name")]
    public string Name { get; set; }

    [DisplayName("Category Description")]
    public string Description { get; set; }

    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }
    public ICollection<CategoryViewModel> Categories { get; set; }
    public ICollection<ProductViewModel> Products { get; set; }
    public ICollection<CategoryViewModel> Subcategories { get; set; }
}
