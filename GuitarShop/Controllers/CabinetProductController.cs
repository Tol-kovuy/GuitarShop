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

    public IActionResult Index()
    {
        var products =  _productService.GetAll();
        var modelList = new List<ProductViewModel>();
        foreach (var product in products)
        {
            var model = _mapper.Map<ProductViewModel>(product);
            modelList.Add(model);
        }
        return View(modelList);
    }

    public IActionResult Edit(int id)
    {
        var product = _productService.GetById(id);
        var model = _mapper.Map<ProductViewModel>(product);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = _mappingProduct.MappingModelToProduct(model);
            await _productService.UpdateAsync(product);
            ViewBag.Message = "Product was update";
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
            await _productService.CreateAsync(product);
            ViewBag.Message = "Product was added";
            return View();
        }
        ModelState.AddModelError("", "Wtf");
        return View();
    }

    public IActionResult Details(int id)
    {
        var product = _productService.GetById(id);
        var model = _mapper.Map<ProductViewModel>(product);
        return View(model);
    }

    public IActionResult Delete(int id, bool rez)
    {
        var product = _productService.GetById(id);
        var model = _mapper.Map<ProductViewModel>(product);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
