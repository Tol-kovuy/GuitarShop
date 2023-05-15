﻿using AutoMapper;
using GuitarShop.BLL.AccountService;
using GuitarShop.BLL.UserService;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GuitarShop.Controllers;

public class AuthenticationController : Controller
{
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IUserService userService,
        IAccountService accountService,
        IMapper mapper
        )
    {
        _userService = userService;
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AuthenticationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(model);
            var response = await _accountService.Login(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response));
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
