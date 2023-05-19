using AutoMapper;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.ProductService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GuitarShop.Controllers;

public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;

    public HomeController(
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
        if (User.IsInRole("User"))
        {
            var count = GetProductCounter();
            ViewBag.Count = count;
        }
        ViewData["Categories"] = GetCategory(); // fuuuuuuuuuuuuuck
        return View();
    }

    [HttpPost]
    public IActionResult Index(string query)
    {
        if (!String.IsNullOrEmpty(query))
        {
            var search = _productService.GetByName(query);
            if (search != null)
            {
                var modelList = new List<ProductViewModel>();
                foreach (var product in search)
                {
                    var model = _mapper.Map<ProductViewModel>(product);
                    modelList.Add(model);
                }
                ViewData["Categories"] = GetCategory();
                ViewData["query"] = modelList;
                return View(modelList);
            }
            
            ViewBag.Message = $"No result with ${query} query!";
            return View();
        }

        return View("Index");
    }

    public IActionResult Catalog(List<ProductViewModel> prod)
    {
        var count = GetProductCounter();
        if (count != default)
        {
            ViewBag.Count = count;
        }
        var allProducts = _productService.GetAll();
        var modelList = new List<ProductViewModel>();
        foreach (var product in allProducts)
        {
            var model = _mapper.Map<ProductViewModel>(product);
            modelList.Add(model);
        }
        ViewData["Categories"] = GetCategory();
        return View(modelList);
    }

    public IActionResult NavigationMenu()
    {
        var categories = _categoryService.GetAll();
        var modelList = new List<CategoryViewModel>();
        foreach (var category in categories)
        {
            var model = _mapper.Map<CategoryViewModel>(category);
            modelList.Add(model);
        }
        ViewBag.Cotegories = GetCategory();
        return PartialView("NavigationMenu", modelList);
    }

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}