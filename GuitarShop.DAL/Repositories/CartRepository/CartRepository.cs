using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.DAL.Repositories.CartRepository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(
        ApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task CreateAsync(Cart entity)
    {
        await _context.Carts.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Cart> GetAll()
    {
        var carts = _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product);
        return carts;
    }

    public async Task UpdateAsync(Cart entity)
    {
        _context.Carts.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteCartItemAsync(CartItem entity)
    {
        _context.CartItems.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Cart entity)
    {
        _context.Carts.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
