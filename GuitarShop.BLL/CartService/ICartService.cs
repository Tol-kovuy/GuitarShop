using GuitarShop.BLL.Dtos;
using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.CartService;

public interface ICartService
{
    Task CreateAsync(Cart cart);
    Task AddToCart(AddToCartDto dto);
    Cart GetByUserId(long id);
    decimal GetTotalPrice(Cart cart);
    Task DeleteCartItem(CartItem cartItem);
    Task DeleteItem(long id);
}
