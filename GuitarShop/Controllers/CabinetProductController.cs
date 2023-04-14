﻿using Azure;
using GuitarShop.BLL.Models;
using GuitarShop.BLL.ProductService;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GuitarShop.Controllers
{
    public class CabinetProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBaseRepository<ProductEntity> _prodRepos;

        public CabinetProductController(
            IProductService productService,
             IBaseRepository<ProductEntity> prodRepos
            )
        {
            _productService = productService;
            _prodRepos = prodRepos;
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
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateAsync(product);
                ViewBag.Message =  response.Description;
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
}
