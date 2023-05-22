using GuitarShop.DAL.Entities;
using System.ComponentModel;

namespace GuitarShop.Models;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentCategoryId { get; set; }
    public CategoryViewModel ParentCategory { get; set; }
}
