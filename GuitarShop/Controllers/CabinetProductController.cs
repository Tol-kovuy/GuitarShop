using AutoMapper;
using GuitarShop.BLL.ProductService;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.Extensions;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers;

public class CabinetProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IBaseRepository<Product> _prodRepos;
    private readonly IMapper _mapper;
    private readonly MappingImageFile _mappingProduct; 

    public CabinetProductController(
        IProductService productService,
         IBaseRepository<Product> prodRepos,
         IMapper mapper,
         MappingImageFile mappingProduct
        )
    {
        _productService = productService;
        _prodRepos = prodRepos;
        _mapper = mapper;
        _mappingProduct = mappingProduct;
    }

    public async Task<IActionResult> Index()
    {
        var allProducts = await _productService.GetAllAsync();
        return View(allProducts);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var products = await _productService.GetAllAsync();
        var product = products.FirstOrDefault(p => p.Id == id);
        var model = _mapper.Map<ProductViewModel>(product);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = _mappingProduct.MappingModelToProduct(model);
            var prod = await _productService.UpdateAsync(product);
            ViewBag.Message = prod.Description;
            return View();
        }

        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = _mappingProduct.MappingModelToProduct(model);
            var response = await _productService.CreateAsync(product);
            ViewBag.Message = response.Description;
            return View();
        }
        ModelState.AddModelError("", "Wtf");
        return View();
    }

    public async Task<IActionResult> Details(int id)
    {
        var products = await _productService.GetAllAsync();
        var product = products.FirstOrDefault(x => x.Id == id);
        return View(product);
    }

    public async Task<IActionResult> Delete(int id, bool rez)
    {
        var products = await _productService.GetAllAsync();
        var prod = products.FirstOrDefault(x => x.Id == id);
        return View(prod);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
