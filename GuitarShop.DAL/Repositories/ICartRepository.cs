using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories
{
    public interface ICartRepository
    {
        Task CreateAsync(Cart entity);
        Task DeleteAsync(Cart entity);
        Task DeleteCartItemAsync(CartItem entity);
        IQueryable<Cart> GetAll();
        Task UpdateAsync(Cart entity);
    }
}