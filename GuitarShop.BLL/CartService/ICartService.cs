using GuitarShop.BLL.Models;

namespace GuitarShop.BLL.CartService;

public interface ICartService
{
    Task<IBaseResponse<bool>> AddToCart(Product product);
    Task<IBaseResponse<bool>> DeleteItem(int id);
}
