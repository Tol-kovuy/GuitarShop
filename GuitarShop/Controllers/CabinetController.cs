using AutoMapper;
using GuitarShop.BLL.AccountService;
using GuitarShop.BLL.UserService;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CabinetController : Controller
{
    private readonly IUserService _userService;
    public CabinetController(
        IUserService userService
        )
    {
        _userService = userService;
    }
    public async Task<IActionResult> Index()
    {
        var currentUser = HttpContext.User.Identity.Name;
        var userInfo = await _userService.GetByNameAsync(currentUser);
        return View(userInfo);
    }
}
