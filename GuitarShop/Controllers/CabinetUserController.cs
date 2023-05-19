using AutoMapper;
using GuitarShop.BLL.Servisec.AccountService;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CabinetUserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly IAccountService _accountService;
    private readonly ICategoryService _categoryService;

    private readonly IMapper _mapper;
    public CabinetUserController(
        IUserService userService,
        IAccountService accountService,
        IMapper mapper,
        ICartService cartService,
        ICategoryService categoryService
        ) : base(userService, cartService, categoryService, mapper)
    {
        _userService = userService;
        _accountService = accountService;
        _mapper = mapper;
        _cartService = cartService;
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        var currentUser = GetCurrentUser();
        var usersEntity = _userService.GetAll(); // НО мне нужно получить всех и сохранить в памяти
        var users = new List<UserViewModel>();
        foreach (var userEntity in usersEntity)
        {
            var user = _mapper.Map<UserViewModel>(userEntity);
            users.Add(user);
        }
        var info = usersEntity.FirstOrDefault(u => u.UserName == currentUser.UserName);
        var model = _mapper.Map<UserViewModel>(info);
        model.Users = users;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        if (ModelState.IsValid)
        {
            await _userService.DeleteAsync(id);
            ModelState.AddModelError("", "User was deleted.");
        }

        return RedirectToAction("Index", "CabinetUser");
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User model)
    {
        if (ModelState.IsValid)
        {
            await _userService.CreateAsync(model);
            return RedirectToAction("Index", "CabinetUser");
        }
        return View(model);
    }
}
