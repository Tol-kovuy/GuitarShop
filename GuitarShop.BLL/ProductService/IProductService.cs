using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.ProductService;

public interface IProductService
{
    Task CreateAsync(Product product);
    IQueryable<Product> GetAll();
    Task DeleteAsync(int id);
    Product GetById(long id);
    IList<Product> GetByName(string name);
    Task UpdateAsync(Product product);
}
