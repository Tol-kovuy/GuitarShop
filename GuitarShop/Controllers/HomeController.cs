using AutoMapper;
using GuitarShop.BLL.CartService;
using GuitarShop.BLL.ProductService;
using GuitarShop.BLL.UserService;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GuitarShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

   public HomeController(
        ILogger<HomeController> logger,
        IProductService productService,
        ICartService cartService,
        IUserService userService,
        IMapper mapper
        )
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
        _userService = userService;
        _mapper = mapper;   
    }
    
    public IActionResult Index() 
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(string query)
    {
        if (!String.IsNullOrEmpty(query))
        {
            var search = await _productService.GetByNameAsync(query);
            
            var list = search.Data;
            if (list != null)
            {
                var modelList = new List<ProductViewModel>();
                ProductViewModel model = new ProductViewModel();
                foreach ( var product in list ) 
                {
                    model = _mapper.Map<ProductViewModel>(product);
                    modelList.Add(model);
                }
                
                return View(modelList);
            }
            ViewBag.Message = search.Description;
            return View();
        }

        return View("Index");
    }

    public async Task<IActionResult> Catalog()
    {
        var allProducts = await _productService.GetAllAsync();
        var modelList = new List<ProductViewModel>();
        foreach (var product in allProducts)
        {
            var model = _mapper.Map<ProductViewModel>(product);
            modelList.Add(model);
        }
        return View(modelList);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}