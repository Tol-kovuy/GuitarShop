using AutoMapper;
using GuitarShop.BLL.Models;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;

namespace GuitarShop;

public class MapperProfile : Profile
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MapperProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
        //CreateMap<Cart, CartEntity>()
        //    .ForMember(dest => dest.UserEntityId, opt => opt.MapFrom(src => src.UserId))
        //    .ForMember(dest => dest.ProductEntities, opt => opt.MapFrom(src => src.Products))
        //    .ForMember(dest => dest.UserEntity, opt => opt.MapFrom(src => src.User));
        CreateMap<RegistrationViewModel, User>();
        CreateMap<AuthenticationViewModel, User>();
        CreateMap<Product, ProductEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => UploadFile(src)));
        CreateMap<ProductEntity, Product>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
           .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.ImageData))
           .ForMember(dest => dest.ImageData, opt => opt.Ignore());
    }
    public MapperProfile(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string UploadFile(Product product)
    {
        string fileName = null;
        if (product.ImageData != null)
        {
            string uploadDir = Path.Combine("C:/Users/Maxim/Documents/GitHub/GuitarShop/GuitarShop/wwwroot/", "img/Catalog");
            fileName = Guid.NewGuid().ToString() + "-" + product.ImageData.FileName;
            string filePath = Path.Combine(uploadDir, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                product.ImageData.CopyTo(fileStream);
            }

        }
        return fileName;
    }
}
