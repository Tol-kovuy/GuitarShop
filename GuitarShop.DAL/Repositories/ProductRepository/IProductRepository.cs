using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories.ProductRepository;

public interface IProductRepository : IBaseRepository<Product>
{
    IQueryable<Product> GetByCategoryId(int categoryId);
}