using AutoMapper;
using GuitarShop.BLL.AccountService;
using GuitarShop.BLL.CartService;
using GuitarShop.BLL.UserService;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CabinetController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    public CabinetController(
        IUserService userService,
        IMapper mapper,
        ICartService cartService)
        : base(userService, cartService)
    {
        _userService = userService;
        _mapper = mapper;
        _cartService = cartService;
    }
    public IActionResult Index()
    {
        var count = GetProductCounter();
        ViewBag.Count = count;
        var currentUser = GetCurrentUser();
        var user = _userService.GetByName(currentUser.UserName);
        var model = _mapper.Map<UserViewModel>(user);
        return View(model);
    }
}
