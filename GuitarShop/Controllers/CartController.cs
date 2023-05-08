using GuitarShop.BLL.CartService;
using GuitarShop.BLL.Dtos;
using GuitarShop.BLL.ProductService;
using GuitarShop.BLL.UserService;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CartController : Controller
{
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public CartController(
        IUserService userService,
        ICartService cartService,
        IProductService productService
        )
    {
        _userService = userService;
        _cartService = cartService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var currentUser = await GetCurrentUser();
        var cart = _cartService.GetByUserId(currentUser.Id);
        if (cart == null)
        {
            return View();
        }
        var cartItems = cart.CartItems.Select(cartItem => new CartItemViewModel
        {
            ProductId = cartItem.ProductId,
            ProductName = cartItem.Product.ProductName,
            ProductDescription = cartItem.Product.ProductDescription,
            Price = cartItem.Product.Price,
            Quantity = cartItem.Quantity,
            ImageData = cartItem.Product.ImageData
        });
        cart = _cartService.GetByUserId(currentUser.Id);
        CartViewModel model = new CartViewModel
        {
            Id = cart.Id,
            CartItems = cartItems,
            TotalPrice = _cartService.GetTotalPrice(cart)
        };
        return View(model);
    }
    public JsonResult GetCartCount()
    {
        int count = 10;
        // Ваш код для получения количества покупок
        return Json(count);
    }

    public async Task<IActionResult> AddItemToCart(long id)
    {
        var product = await _productService.GetByIdAsync(id);
        var currentUser = await GetCurrentUser();
        var dto = new AddToCartDto()
        {
            UserId = currentUser.Id,
            ProductId = product.Id
        };
        await _cartService.AddToCart(dto);
        return RedirectToAction("Index");
    }

    //[HttpPost]
    public async Task<IActionResult> CleanCart()
    {
        var currentUser = await GetCurrentUser();
        var currentCart = _cartService.GetByUserId(currentUser.Id);
        await _cartService.DeleteItem(currentCart.Id);
        return View("Index");
    }

    public async Task<IActionResult> DeleteOneProductFromCart(long id)
    {
        var currentUser = await GetCurrentUser();
        var currentCart = _cartService.GetByUserId(currentUser.Id);
        var findCartItemByProduct = currentCart.CartItems.SingleOrDefault(x => x.ProductId == id);
        await _cartService.DeleteCartItem(findCartItemByProduct);
        return RedirectToAction("Index");
    }

    private async Task<User> GetCurrentUser()
    {
        var currentUserName = this.User.Identity.Name;
        var currentUser = await _userService.GetByNameAsync(currentUserName);
        return currentUser;
    }
}
