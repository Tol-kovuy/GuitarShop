using AutoMapper;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class ControllerBase : Controller
{
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ControllerBase(
        IUserService userService,
        ICartService cartService,
        ICategoryService categoryService,
        IMapper mapper
        )
    {
        _userService = userService;
        _cartService = cartService;
        _categoryService = categoryService;
        _mapper = mapper;
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

        if (currentUser != null && User.IsInRole("User"))
        {
            var cart = _cartService.GetByUserId(currentUser.Id);
            if (cart != null)
            {
                foreach (var cartItem in cart.CartItems)
                {
                    count += cartItem.Quantity;
                }
            }
        }
        return count;
    }


    public IList<CategoryViewModel> GetCategories()
    {
        var modelLinq = _categoryService
            .GetAll()
            .Select(category => _mapper.Map<CategoryViewModel>(category))
            .ToList();

        return modelLinq;
    }
}
