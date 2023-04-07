using AutoMapper;
using GuitarShop.BLL.Enum;
using GuitarShop.BLL.Models;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            }; // throw???
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
