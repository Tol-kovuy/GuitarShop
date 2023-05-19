using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Repositories;

namespace GuitarShop.BLL.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IProductRepository productRepository
        )
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task CreareAsync(Category category)
    {
        await _categoryRepository.CreateAsync(category);
    }

    public IQueryable<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public Category GetById(long id)
    {
        var category = _categoryRepository.GetAll().SingleOrDefault(c => c.Id == id);
        return category;
    }

    public Category GetByName(string name)
    {
        var category = _categoryRepository.GetByName(name);
        return category;
    }

    public IList<Product> GetProductByCotegoryId(long id)
    {
        var category = GetById(id);
        var product = _productRepository.GetAll().Where(p => p.Category.Equals(category)).ToList();
        return product;
    }
}
