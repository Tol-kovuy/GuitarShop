using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories.CategoryRepository;

public interface ICategoryRepository : IBaseRepository<Category>
{
    // todo: remove use id
    Category GetByName(string name);
}