using AutoMapper;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class CabinetCategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CabinetCategoryController(
            ICategoryService categoryService,
            IMapper mapper
            )
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Category>(model);
                await _categoryService.CreareAsync(entity);
                return RedirectToAction("Index", "CabinetProduct");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddSubCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category entityCategory = null;
                var existCategory = _categoryService.GetByName(model.ParentCategory.Name);
                var existSubCategories = _categoryService.GetAll().Where(x => x.ParentCategoryId == existCategory.Id);
                if (existCategory != null)
                {
                    var parentCategory = new Category
                    {
                        Id = existCategory.Id,
                        Name = existCategory.Name,
                        Description = existCategory.Description
                    };
                    entityCategory = new Category
                    {
                        Name = model.Name,
                        Description = model.Description,
                        ParentCategory = parentCategory
                    };
                }
                await _categoryService.AddSubCategoryAsync(entityCategory);
                return RedirectToAction("Index", "CabinetProduct");
            }
            return View();
        }
    }
}
