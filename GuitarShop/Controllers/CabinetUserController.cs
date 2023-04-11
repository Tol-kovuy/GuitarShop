using AutoMapper;
using GuitarShop.BLL.AccountService;
using GuitarShop.BLL.Models;
using GuitarShop.BLL.UserService;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CabinetUserController : Controller
{
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    public CabinetUserController(
        IUserService userService,
        IAccountService accountService,
        IMapper mapper
        )
    {
        _userService = userService;
        _accountService = accountService;
        _mapper = mapper;
    }

    //public IActionResult Index()
    //{
    //    return View();
    //}

    public async Task<IActionResult> Index()
    {
        var currentUser = HttpContext.User;
        var users = await _userService.GetAllAsync();
        var info = users.FirstOrDefault(u => u.UserName == currentUser.Identity.Name);
        var collections = new User
        {
            Id = info.Id,
            UserName = info.UserName,
            FirstName = info.FirstName,
            LastName = info.LastName,
            Email = info.Email,
            Password = info.Password,
            Role = info.Role,
            Users = users,
        };
        return View(collections);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.DeleteAsync(id);
            ModelState.AddModelError("", response.Description);
        }

        return RedirectToAction("Index", "CabinetUser");
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User model)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.CreateAsync(model);
            if (response.StatusCode == BLL.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "CabinetUser");
            }
            ModelState.AddModelError("", response.Description);
        }
        return View(model);
    }
}
