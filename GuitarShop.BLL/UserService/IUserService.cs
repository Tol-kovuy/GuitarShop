using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.UserService;

public interface IUserService
{
    Task CreateAsync(User user);
    IQueryable<User> GetAll();
    Task DeleteAsync(long id);
    Task<User> GetByIdAsync(long id);
    User GetByName(string name);
}