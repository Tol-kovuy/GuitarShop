using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.CategoryService;

public interface ICategoryService
{
    Task CreareAsync(Category category);
    IQueryable<Category> GetAll();
    Category GetById(long id);
    IList<Product> GetProductByCotegoryId(long id);
}
