using GuitarShop.BLL.Models;

namespace GuitarShop.BLL.ProductService;

public interface IProductService
{
    Task<IBaseResponse<Product>> CreateAsync(Product product);
    Task<IBaseResponse<bool>> DeleteAsync(int id);
    Task<IList<Product>> GetAllAsync();
    Task<IBaseResponse<IList<Product>>> GetByNameAsync(string name);
    Task<IBaseResponse<bool>> UpdateAsync(Product product);
}
