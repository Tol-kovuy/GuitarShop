using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(
        ApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task CreateAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Product> GetAll()
    {
        return _context.Products.Include(c => c.Category);
    }

    public async Task UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Product entity)
    {
        _context.Products.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Product> GetByCategoryId(int categoryId)
    {
        var products = _context.Products.Where(x => x.CategoryId == categoryId);
        return products;
    }
}
