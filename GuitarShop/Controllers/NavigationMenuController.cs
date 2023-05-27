using AutoMapper;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.ProductService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.DAL.Entities;
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
        var categoriesLinq = _categoryService.GetAll().Select(m => _mapper.Map<CategoryViewModel>(m)).ToList();

        //var categories = _categoryService.GetAll();
        //var modelList = new List<CategoryViewModel>();
        //foreach (var category in categories)
        //{
        //    var model = _mapper.Map<CategoryViewModel>(category);
        //    modelList.Add(model);
        //}
        return View(categoriesLinq);
    }

    public IActionResult GetProductsByCategoryId(int categoryId)
    {
        var products = _productService.GetAll()
            .Where(c => c.Category.Id == categoryId)
            .Select(m => _mapper.Map<ProductViewModel>(m))
            .ToList();

        //var modelList = new List<ProductViewModel>();
        //foreach (var product in products)
        //{
        //    var model = _mapper.Map<ProductViewModel>(product);
        //    modelList.Add(model);
        //}

        ViewData["Categories"] = GetCategory();
        return View(products);
    }
    public IActionResult GetProductsByCategoryName(string categoryName)
    {
        var category = _categoryService.GetByName(categoryName);
        var subCategories = _categoryService.GetAll().Where(parentCategoryId => parentCategoryId.ParentCategoryId == category.Id);
        List<ProductViewModel> modelList = null;
        foreach (var subCategory in subCategories)
        {
            modelList = _productService
                .GetByCategoryId(subCategory.Id)
                .Select(product => _mapper.Map<ProductViewModel>(product))
                .ToList();
        }
        ViewData["Categories"] = GetCategory();
        return View("GetProductsByCategoryId", modelList);
    }
}
