using AutoMapper;
using GuitarShop.BLL.CartService;
using GuitarShop.BLL.CategoryService;
using GuitarShop.BLL.ProductService;
using GuitarShop.BLL.UserService;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class NavigationMenuController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;

    public NavigationMenuController(
        ILogger<HomeController> logger,
        IProductService productService,
        ICartService cartService,
        IUserService userService,
        IMapper mapper,
        ICategoryService categoryService
        ) : base(userService, cartService, categoryService, mapper)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
        _userService = userService;
        _mapper = mapper;
        _categoryService = categoryService;
    }
    public IActionResult Index()
    {
        var categories = _categoryService.GetAll();
        var modelList = new List<CategoryViewModel>();
        foreach (var category in categories)
        {
            var model = _mapper.Map<CategoryViewModel>(category);
            modelList.Add(model);
        }
        return View(modelList);
    }

    public IActionResult GetProductsByCategory(long id)
    {
        var products = _productService.GetAll().Where(c => c.Category.Id == id);
        var modelList = new List<ProductViewModel>();
        foreach (var product in products)
        {
            var model = _mapper.Map<ProductViewModel>(product);
            modelList.Add(model);
        }
        ViewData["Categories"] = GetCategory();
        return View(modelList);
    }
}
