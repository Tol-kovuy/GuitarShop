using GuitarShop.BLL.CartService;
using GuitarShop.BLL.UserService;
using GuitarShop.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class ControllerBase : Controller
{
    private readonly IUserService _userService;
    private readonly ICartService _cartService;

    public ControllerBase(
        IUserService userService, 
        ICartService cartService
        )
    {
        _userService = userService;
        _cartService = cartService;
    }
    public User GetCurrentUser()
    {
        var currentUserName = this.User.Identity.Name;
        var currentUser = _userService.GetByName(currentUserName);
        return currentUser;
    }

    public int GetProductCounter()
    {
        int count = 0;
        var currentUser = this.GetCurrentUser();
        if (currentUser !=null)
        {
            var cart = _cartService.GetByUserId(currentUser.Id);
            foreach (var cartItem in cart.CartItems)
            {
                count += cartItem.Quantity;
            }
        }
        return count;
    }
}
