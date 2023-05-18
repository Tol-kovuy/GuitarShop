using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;

public class CategoryRepository : IBaseRepository<Category>
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(
        ApplicationDbContext context
        )
    {
        _context = context;
    }
    public async Task CreateAsync(Category entity)
    {
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category entity)
    {
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Category> GetAll()
    {
        var categories = _context.Categories;
        return categories;
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }
}
