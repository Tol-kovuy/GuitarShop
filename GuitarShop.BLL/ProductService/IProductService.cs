using GuitarShop.BLL.Models;

namespace GuitarShop.BLL.ProductService;

public interface IProductService
{
    Task<IBaseResponse<Product>> CreateAsync(Product product);
    Task<BaseResponse<bool>> DeleteAsync(string productName);
    Task<IList<Product>> GetAllAsync();
    Task<IBaseResponse<IList<Product>>> GetByNameAsync(string name);
}
