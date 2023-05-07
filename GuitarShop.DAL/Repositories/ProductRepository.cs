using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;

public class ProductRepository : IBaseRepository<Product>
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
        return _context.Products;
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

}
