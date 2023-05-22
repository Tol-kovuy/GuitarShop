using AutoMapper;
using GuitarShop.BLL.Dtos;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.ProductService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CartController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public CartController(
        IUserService userService,
        ICartService cartService,
        IProductService productService,
        IMapper mapper,
        ICategoryService categoryService
        ) : base(userService, cartService, categoryService, mapper)
    {
        _userService = userService;
        _cartService = cartService;
        _productService = productService;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        var count = GetProductCounter();
        if (count != default)
        {
            ViewBag.Count = count;
        }
        var currentUser = GetCurrentUser();
        var cart = _cartService.GetByUserId(currentUser.Id);
        if (cart == null)
        {
            return View();
        }
        var model = _mapper.Map<CartViewModel>(cart);
        return View(model);
    }

    public async Task<IActionResult> AddItemToCart(long id)
    {
        var product = _productService.GetById(id);
        var currentUser = GetCurrentUser();
        var dto = new AddToCartDto()
        {
            UserId = currentUser.Id,
            ProductId = product.Id
        };
        await _cartService.AddToCart(dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> CleanCart()
    {
        var currentUser = GetCurrentUser();
        var currentCart = _cartService.GetByUserId(currentUser.Id);
        await _cartService.DeleteItem(currentCart.Id);
        return View("Index");
    }

    public async Task<IActionResult> DeleteOneProductFromCart(long id)
    {
        var currentUser = GetCurrentUser();
        var currentCart = _cartService.GetByUserId(currentUser.Id);
        var findCartItemByProduct = currentCart.CartItems.SingleOrDefault(x => x.Product.Id == id);
        await _cartService.DeleteCartItem(findCartItemByProduct);
        return RedirectToAction("Index");
    }
}
