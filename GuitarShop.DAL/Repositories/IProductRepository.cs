using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    IQueryable<Product> GetByCategoryId(int categoryId);
}