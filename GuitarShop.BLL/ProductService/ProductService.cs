using AutoMapper;
using GuitarShop.BLL.Exceptions;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GuitarShop.BLL.ProductService;


public class ProductService : IProductService
{
    private readonly IBaseRepository<Product> _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
         IBaseRepository<Product> productRepository, 
         ILogger<ProductService> logger
        )
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task CreateAsync(Product product)
    {
        try
        {
            var prod = _productRepository.GetAll().FirstOrDefault(p => p.Name == product.Name);
            if (prod != null)
            {
                throw new ProductException($"Product witn {product.Name} already exist");
            }
            await _productRepository.CreateAsync(product);
        }
        catch (ProductException ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public IQueryable<Product> GetAll()
    {
        IQueryable<Product> products;
        try
        {
            products = _productRepository.GetAll();
            if (products == null)
            {
                throw new ProductException("No products");
            }
        }
        catch (ProductException ex)
        {
            _logger.LogError(ex, "Error", ex);
            throw;
        }
        return products;
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var prod = _productRepository.GetAll().FirstOrDefault(p => p.Id == id);
            if (prod == null)
            {
                throw new ProductException($"No product with {id} id");
            }
            await _productRepository.DeleteAsync(prod);
        }
        catch (ProductException ex)
        {
            _logger.LogError(ex, "Error", ex.Message);
            throw;
        }
    }

    public Product GetById(long id)
    {
        Product product = null;
        try
        {
            product = _productRepository.GetAll().FirstOrDefault(product => product.Id == id);
            if (product == null)
            {
                throw new ProductException($"No product with {id} id");
            }
        }
        catch (ProductException ex)
        {
            _logger.LogError(ex, "Error", ex.Message);
            throw;
        }
       
        return product;
    }

    public IList<Product> GetByName(string name)
    {
        try
        {
            var prodList = _productRepository.GetAll().Where(p => p.Name.Contains(name.ToUpperInvariant())).ToList();
            if (prodList.Count == 0)
            {
                throw new ProductException("Products not found");
            }
            return prodList;
        }
        catch (ProductException ex)
        {
            _logger.LogError(ex, "Error", ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Product product)
    {
        try
        {
            var productExist = _productRepository.GetAll().FirstOrDefault(p => p.Id == product.Id);
            if (productExist == null)
            {
                throw new ProductException("Product not found");
            }
            await _productRepository.UpdateAsync(product);
        }
        catch (ProductException ex)
        {
            _logger.LogError(ex, "Error", ex.Message);
            throw;
        }
    }
}
