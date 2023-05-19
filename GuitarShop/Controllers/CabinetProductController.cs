using AutoMapper;
using GuitarShop.BLL.CartService;
using GuitarShop.BLL.CategoryService;
using GuitarShop.BLL.ProductService;
using GuitarShop.BLL.UserService;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.Extensions;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuitarShop.Controllers;

public class CabinetProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IBaseRepository<Product> _productRepos;
    private readonly IMapper _mapper;
    private readonly MappingImageFile _mappingProduct;
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;

    public CabinetProductController(
        IProductService productService,
        IBaseRepository<Product> prodRepos,
        IMapper mapper,
        MappingImageFile mappingProduct,
        IUserService userService,
        ICartService cartService,
        ICategoryService categoryService
        ) : base(userService, cartService, categoryService, mapper)
    {
        _productService = productService;
        _productRepos = prodRepos;
        _mapper = mapper;
        _mappingProduct = mappingProduct;
        _userService = userService;
        _cartService = cartService;
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAll();
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
        var categories = GetCategory();
        var subCategoryList = categories.Where(x => x.ParentCategoryId != null).Select(x => x.Name);
        ViewBag.subCategoryList = subCategoryList;
        var categoryList = categories.Where(x => x.ParentCategoryId == null).Select(x => x.Name);
        ViewBag.categoryList = categoryList;
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
