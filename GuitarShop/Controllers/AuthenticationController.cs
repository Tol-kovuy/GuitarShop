using AutoMapper;
using GuitarShop.BLL.Servisec.AccountService;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.ProductService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Repositories.ProductRepository;
using GuitarShop.Extensions;
using GuitarShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GuitarShop.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepos;
    private readonly IMapper _mapper;
    private readonly MappingImageFile _mappingProduct;
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    private readonly IAccountService _accountService;

    public AuthenticationController(
        IProductService productService,
        IProductRepository prodRepos,
        IMapper mapper,
        MappingImageFile mappingProduct,
        IUserService userService,
        ICartService cartService,
        ICategoryService categoryService,
        IAccountService accountService) 
        : base(userService, cartService, categoryService, mapper)
    {
        _productService = productService;
        _productRepos = prodRepos;
        _mapper = mapper;
        _mappingProduct = mappingProduct;
        _userService = userService;
        _cartService = cartService;
        _categoryService = categoryService;
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        ViewData["Categories"] = GetCategories();
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
