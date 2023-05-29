using AutoMapper;
using GuitarShop.BLL.Servisec.CartService;
using GuitarShop.BLL.Servisec.CategoryService;
using GuitarShop.BLL.Servisec.UserService;
using GuitarShop.Controllers;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;

namespace GuitarShop.Extensions;

// TODO: use interface + DI
public class MappingImageFile : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserService _userService;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public MappingImageFile(
        IWebHostEnvironment webHostEnvironment,
        IUserService userService,
        ICartService cartService,
        ICategoryService categoryService,
        IMapper mapper
        ) : base(userService, cartService, categoryService, mapper)
    {
        _webHostEnvironment = webHostEnvironment;
        _userService = userService;
        _cartService = cartService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public Product MappingModelToProduct(ProductViewModel model)
    {
        string fileName = null;
        if (model.ImageData != null)
        {
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img/Catalog");
            fileName = Guid.NewGuid().ToString() + "-" + model.ImageData.FileName;
            string filePath = Path.Combine(uploadDir, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageData.CopyTo(fileStream);
            }
        }
        var product = new Product
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Quantity = model.Quantity,
            ImageData = fileName,
            CategoryId = GetCategoty(model.Category).Id
        };
        return product;
    }

    public Category GetCategoty(CategoryViewModel model)
    {
        var categoryLinq = GetCategories()
            .Where(n => n.Name == model.Name)
            .Select(m => _mapper.Map<Category>(m))
            .FirstOrDefault();

        return categoryLinq;
    }
}
