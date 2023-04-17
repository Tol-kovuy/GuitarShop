using GuitarShop.BLL.ProductService;
using GuitarShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GuitarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService
            )
        {
            _logger = logger;
            _productService = productService;
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
                    return View(list);
                }
                ViewBag.Message = search.Description;
                return View();
            }

            return View("Index");
        }

        public async Task<IActionResult> Catalog()
        {
            var allProducts = await _productService.GetAllAsync();
            return View(allProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}