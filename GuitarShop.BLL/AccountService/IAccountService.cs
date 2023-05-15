using GuitarShop.DAL.Entities;
using System.Security.Claims;

namespace GuitarShop.BLL.AccountService;

public interface IAccountService
{
    Task<ClaimsIdentity> Login(User user);
    Task<ClaimsIdentity> Register(User user);
}
