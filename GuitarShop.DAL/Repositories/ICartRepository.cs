using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;


public interface ICartRepository : IBaseRepository<Cart>
{
    Task DeleteCartItemAsync(CartItem entity);
}