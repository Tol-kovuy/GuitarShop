using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.UserService;

public interface IUserService
{
    Task<IBaseResponse<User>> CreateAsync(User user);
    Task<BaseResponse<bool>> DeleteAsync(long id);
    Task<IList<User>> GetAllAsync();
    Task<User> GetByIdAsync(long id);
    Task<User> GetByNameAsync(string name);
}