using GuitarShop.DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace GuitarShop.DAL.Entities;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
