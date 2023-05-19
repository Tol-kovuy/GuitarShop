using AutoMapper;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CabinetController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;

    public CabinetController(
        IUserService userService,
        IMapper mapper,
        ICartService cartService,
        ICategoryService categoryService)
        : base(userService, cartService, categoryService, mapper)
    {
        _userService = userService;
        _mapper = mapper;
        _cartService = cartService;
        _categoryService = categoryService;
    }
    public IActionResult Index()
    {
        if (User.IsInRole("User"))
        {
            var count = GetProductCounter();
            ViewBag.Count = count;
        }
        ViewData["Categories"] = GetCategory();
        var currentUser = GetCurrentUser();
        var user = _userService.GetByName(currentUser.UserName);
        var model = _mapper.Map<UserViewModel>(user);
        return View(model);
    }
}
