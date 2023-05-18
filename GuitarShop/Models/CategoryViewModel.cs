using System.ComponentModel;

namespace GuitarShop.Models;

public class CategoryViewModel
{
    public long Id { get; set; }

    [DisplayName("Category Name")]
    public string Name { get; set; }

    [DisplayName("Category Description")]
    public string Description { get; set; }
    public ICollection<ProductViewModel> Products { get; set; }
}
