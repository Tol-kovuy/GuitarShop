using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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
        RecursiveDelete(entity);
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

    // todo: use cascade delete?
    private void RecursiveDelete(Category parent)
    {
        if (parent.Name != null)
        {
            var children = _context.Categories
                .Where(x => x.ParentCategoryId == parent.Id);

            foreach (var child in children)
            {
                RecursiveDelete(child);
            }
        }
        if (parent.ParentCategory != null)
        {
            _context.Categories.Remove(parent.ParentCategory);
            _context.Categories.Remove(parent);
        }
        else
        {
            _context.Categories.Remove(parent);
        }
    }
}
