using AutoMapper;
using GuitarShop.BLL.CartService;
using GuitarShop.BLL.CategoryService;
using GuitarShop.BLL.UserService;
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
            foreach (var cartItem in cart.CartItems)
            {
                count += cartItem.Quantity;
            }
        }
        return count;
    }

    public IList<CategoryViewModel> GetCategory()
    {
        var categories = _categoryService.GetAll();
        var listModel = new List<CategoryViewModel>();
        foreach (var category in categories)
        {
            var model = _mapper.Map<CategoryViewModel>(category);
            listModel.Add(model);
        }
        return listModel;
    }
}
