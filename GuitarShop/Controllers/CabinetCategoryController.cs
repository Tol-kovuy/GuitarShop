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
            var categories = _categoryService.GetAll();
            var listModel = new List<CategoryViewModel>();
            if (categories == null)
            {
                return View();
            }
            foreach (var category in categories)
            {
                var model = _mapper.Map<CategoryViewModel>(category);
                listModel.Add(model);
            }
            ViewData["Categories"] = listModel;
            return View(listModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            var existCategory = _categoryService.GetAll().SingleOrDefault(c => c.Name == model.Name);
            if (existCategory != null)
            {
                ViewBag.CategoryExist = $"Category with '{model.Name}' name already exist!";
                return View("Create");
            }
            if (model.Id != default)
            {
                var subCategory = new Category
                {
                    ParentCategoryId = model.Id,
                    Name = model.Name,
                    Description = model.Description
                };
                await _categoryService.AddSubCategoryAsync(subCategory);
            }
            else
            {
                var entityCategory = _mapper.Map<Category>(model);
                await _categoryService.CreareAsync(entityCategory);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteById(int id)
        {
            await _categoryService.DeleteById(id);
            return RedirectToAction("Index");
        }

    }
}
