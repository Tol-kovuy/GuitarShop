using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.DAL.Repositories;

public class CartRepository : IBaseRepository<CartEntity>
{
    private readonly ApplicationDbContext _context;

    public CartRepository(
        ApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task CreateAsync(CartEntity entity)
    {
        await _context.CartEntities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<CartEntity> GetAll()
    {
        return _context.CartEntities;
    }

    public async Task UpdateAsync(CartEntity entity)
    {
        _context.CartEntities.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(CartEntity entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
