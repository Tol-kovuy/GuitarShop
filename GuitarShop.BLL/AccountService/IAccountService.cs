using GuitarShop.DAL.Entities;
using System.Security.Claims;

namespace GuitarShop.BLL.AccountService;

public interface IAccountService
{
    Task<IBaseResponse<ClaimsIdentity>> Login(User user);
    Task<BaseResponse<ClaimsIdentity>> Register(User user);
}
