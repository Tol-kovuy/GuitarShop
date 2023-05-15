using AutoMapper;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;

namespace GuitarShop.Extensions;

// TODO: use interface + DI
public class MappingImageFile
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MappingImageFile(
        IWebHostEnvironment webHostEnvironment
        )
    {
        _webHostEnvironment = webHostEnvironment;
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
            ImageData = fileName
        };
        return product;
    }
}
