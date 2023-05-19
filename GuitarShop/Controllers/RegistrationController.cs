using AutoMapper;
using GuitarShop.BLL.Servisec.AccountService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GuitarShop.Controllers;

public class RegistrationController : Controller
{
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public RegistrationController(
        IUserService userService,
        IAccountService accountService,
        IMapper mapper)
    {
        _userService = userService;
        _accountService = accountService;
        _mapper = mapper;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newUser = _mapper.Map<User>(model);
            var response = await _accountService.Register(newUser);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response));
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }
}
