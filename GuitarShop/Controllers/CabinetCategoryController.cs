using AutoMapper;
using GuitarShop.BLL.CategoryService;
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
    }
}
