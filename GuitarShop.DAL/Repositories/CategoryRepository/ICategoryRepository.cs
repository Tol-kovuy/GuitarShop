using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories.CategoryRepository;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Category GetByName(string name);
}