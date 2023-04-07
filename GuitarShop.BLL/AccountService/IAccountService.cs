﻿using GuitarShop.BLL.Models;
using System.Security.Claims;

namespace GuitarShop.BLL.AccountService;

public interface IAccountService
{
    Task<IBaseResponse<ClaimsIdentity>> Login(User user);
}