using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories.CartRepository;


public interface ICartRepository : IBaseRepository<Cart>
{
    Task DeleteCartItemAsync(CartItem entity);
}