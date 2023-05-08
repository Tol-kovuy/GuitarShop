using AutoMapper;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.BLL.ProductService;

public class ProductService : IProductService
{
    private readonly IBaseRepository<DAL.Entities.Product> _productRepository;
    private readonly IMapper _mapper;
    private IList<Product> _products;

    public ProductService(
         IBaseRepository<DAL.Entities.Product> productRepository,
         IMapper mapper
        )
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IBaseResponse<Product>> CreateAsync(Product product)
    {
        try
        {
            var products = await GetAllAsync();
            var prod = products.FirstOrDefault(p => p.ProductName == product.ProductName);
            if (prod != null)
            {
                return new BaseResponse<Product>
                {
                    Data = product,
                    Description = $"Products with name '{product.ProductName}' already exist.",
                    StatusCode = Enum.StatusCode.ProductAlreadyExists
                };
            }
            var newProduct = _mapper.Map<DAL.Entities.Product>(product);
            await _productRepository.CreateAsync(newProduct);
            return new BaseResponse<Product>()
            {
                Data = product,
                StatusCode = Enum.StatusCode.OK,
                Description = $"Products '{product.ProductName}' added."
            };
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IBaseResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            var products = await GetAllAsync();
            var prod = products.FirstOrDefault(p => p.Id == id);
            if (prod == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = Enum.StatusCode.ProductNotFound,
                    Description = $"Products with '{id}' ID not found."
                };
            }
            var prodEntity = _mapper.Map<DAL.Entities.Product>(prod);
            await _productRepository.DeleteAsync(prodEntity);
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = Enum.StatusCode.OK,
                Description = $"Products '{prod.ProductName}' was deleted."
            };
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IList<Product>> GetAllAsync()
    {
        try
        {
            if (_products == null)
            {
                _products = new List<Product>();
                var allprodutsFromDb = await _productRepository.GetAll().ToListAsync();
                foreach (var product in allprodutsFromDb)
                {
                    _products.Add(_mapper.Map<Product>(product));
                }
            }
            return _products;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Product> GetByIdAsync(long id)
    {
        var products = await GetAllAsync();
        var product = products.FirstOrDefault(product => product.Id == id);
        return product;
    }

    public async Task<IBaseResponse<IList<Product>>> GetByNameAsync(string name)
    {
        try
        {
            var products = await GetAllAsync();
            var prod = products.Where(p => p.ProductName.ToUpperInvariant().ToLower().Contains(name.ToUpperInvariant().ToLower())).ToList();
            if (prod.Count == 0)
            {
                return new BaseResponse<IList<Product>>()
                {
                    StatusCode = Enum.StatusCode.ProductNotFound,
                    Description = $"No result with '{name}'."
                };
            }
            return new BaseResponse<IList<Product>>()
            {
                Data = prod,
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IBaseResponse<bool>> UpdateAsync(Product product)
    {
        var products = await GetAllAsync();
        var productExist = products.FirstOrDefault(p => p.Id == product.Id);
        if (productExist == null)
        {
            return new BaseResponse<bool>()
            {
                Data = false,
                Description = $"Products with id '{product.Id}' was not found.",
                StatusCode = Enum.StatusCode.ProductNotFound
            };
        }
        var productEntity = _mapper.Map<DAL.Entities.Product>(product);
        await _productRepository.UpdateAsync(productEntity);
        return new BaseResponse<bool>()
        {
            StatusCode = Enum.StatusCode.OK,
            Data = true,
            Description = "Products was updated."
        };
    }
}
