﻿using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Repositories.CategoryRepository;
using GuitarShop.DAL.Repositories.ProductRepository;
using System.Xml;

namespace GuitarShop.BLL.Servisec.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IProductRepository productRepository
        )
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task AddSubCategoryAsync(Category category)
    {
        await _categoryRepository.UpdateAsync(category);
    }

    // todo :rename fix name
    public async Task CreateAsync(Category category)
    {
        await _categoryRepository.CreateAsync(category);
    }

    public async Task DeleteById(int id)
    {
        var category = GetAll().SingleOrDefault(c => c.Id == id);
        if(category == null)
        {
            throw new Exception($"No category with '{id}' id.");
        }
        await _categoryRepository.DeleteAsync(category);
    }

    // todo: remove use id
    public async Task DeleteByNama(string name)
    {
        var category = GetAll().SingleOrDefault(c => c.Name == name);
        if (category == null)
        {
            throw new Exception($"No category with '{name}' name.");
        }
        await _categoryRepository.DeleteAsync(category);
    }

    public IQueryable<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public Category GetById(long id)
    {
        var category = _categoryRepository.GetAll().SingleOrDefault(c => c.Id == id);
        return category;
    }

    public Category GetByName(string name)
    {
        var category = _categoryRepository.GetByName(name);
        return category;
    }

    public IList<Product> GetProductByCotegoryId(long id)
    {
        var category = GetById(id);
        var product = _productRepository.GetAll().Where(p => p.Category.Equals(category)).ToList();
        return product;
    }
}
