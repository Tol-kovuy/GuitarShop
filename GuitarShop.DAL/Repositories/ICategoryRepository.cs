using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Category GetByName(string name);
}