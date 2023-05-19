using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.ProductService;

public interface IProductService
{
    Task CreateAsync(Product product);
    Product GetById(long id);
    IList<Product> GetByName(string name);
    IList<Product> GetByCategoryId(int categoryId);
    IQueryable<Product> GetAll();
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
