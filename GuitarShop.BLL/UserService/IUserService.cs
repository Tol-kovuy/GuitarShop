using GuitarShop.BLL.Models;

namespace GuitarShop.BLL.UserService
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        void Delete(long id);
        IList<User> GetAll();
        User GetById(int id);
    }
}