using GuitarShop.BLL.Models;

namespace GuitarShop.BLL.UserService
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> CreateAsync(User user);
        Task<BaseResponse<bool>> DeleteAsync(long id);
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
    }
}