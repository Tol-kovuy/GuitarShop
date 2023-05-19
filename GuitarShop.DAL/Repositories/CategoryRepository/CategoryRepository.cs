using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories.CategoryRepository;

public class CategoryRepository : ICategoryRepository
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

    public Category GetByName(string name)
    {
        var category = _context.Categories.SingleOrDefault(c => c.Name == name);
        return category;
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }
}
