using AutoMapper;
using GuitarShop.BLL.Enum;
using GuitarShop.BLL.Models;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GuitarShop.BLL.AccountService;

public class AccountService : IAccountService
{
    private readonly IBaseRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public AccountService(
        IBaseRepository<UserEntity> userRepository,
        IMapper mapper
        )
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IBaseResponse<ClaimsIdentity>> Login(User user)
    {
        try
        {
            var userEntity = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (userEntity == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"User name '{user.UserName}' is wrong. Please registred!"
                };
            }
            else if (user.Password != userEntity.Password)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Password is not correct."
                };
            }
            var authUser = _mapper.Map<User>(userEntity);
            var result = Authenticate(authUser);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK,
                Description = $"{user.FirstName} {user.LastName}, welcome to Guitar Shop!"
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> Register(User user)
    {
        try
        {
            var userCheck = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (userCheck != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"User with {user.UserName} name already exists. Please change user name."
                };
            }
            var newUser = _mapper.Map<UserEntity>(user);
            await _userRepository.CreateAsync(newUser);
            var claimIdentity = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = claimIdentity,
                StatusCode = StatusCode.OK,
                Description = $"{user.UserName} was registred!"
            };
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
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
        return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
