using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.Servisec.CategoryService;

public interface ICategoryService
{
    Task CreareAsync(Category category);
    Task AddSubCategoryAsync(Category category);
    Category GetById(long id);
    Category GetByName(string name);
    IQueryable<Category> GetAll();
    IList<Product> GetProductByCotegoryId(long id);
    Task DeleteById(int id);
    Task DeleteByNama(string name);
}
