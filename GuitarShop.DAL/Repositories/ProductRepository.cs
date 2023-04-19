using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;

public class ProductRepository : IBaseRepository<ProductEntity>
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(
        ApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task CreateAsync(ProductEntity entity)
    {
        await _context.ProductEntities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<ProductEntity> GetAll()
    {
        return _context.ProductEntities;
    }

    public async Task UpdateAsync(ProductEntity entity)
    {
        _context.ProductEntities.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(ProductEntity entity)
    {
        _context.ProductEntities.Remove(entity);
        await _context.SaveChangesAsync();
    }

}
