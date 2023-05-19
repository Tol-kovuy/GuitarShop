using AutoMapper;
using GuitarShop.BLL.Enum;
using GuitarShop.BLL.Exceptions;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Repositories;
using GuitarShop.DAL.Repositories.CartRepository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GuitarShop.BLL.Servisec.AccountService;

// TODO: use exceptions
public class AccountService : IAccountService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public AccountService(
        IBaseRepository<User> userRepository,
        IMapper mapper,
        ICartRepository cartRepository
        )
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ClaimsIdentity> Login(User user)
    {
        try
        {
            var userEntity = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (userEntity == null)
            {
                throw new UserException($"User name '{user.UserName}' is wrong. Please registred!");
            }
            else if (user.Password != userEntity.Password)
            {
                throw new UserException("Password is not correct.");
            }
            var result = Authenticate(userEntity);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ClaimsIdentity> Register(User user)
    {
        try
        {
            var userCheck = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (userCheck != null)
            {
                throw new UserException($"User with {user.UserName} name already exists. Please change user name.");
            }
            await _userRepository.CreateAsync(user);
            var claimIdentity = Authenticate(user);
            return claimIdentity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleType.ToString())
            };
        return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
