using AutoMapper;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;

namespace GuitarShop;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegistrationViewModel, User>();
        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleType))
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<AuthenticationViewModel, User>();
        CreateMap<Product, ProductViewModel>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.ImageData))
           .ForMember(dest => dest.ImageData, opt => opt.Ignore())
           .ForPath(dest => dest.Category.Id, opt => opt.MapFrom(src => src.Category.Id))
           .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.Category.Name))
           .ForPath(dest => dest.Category.Description, opt => opt.MapFrom(src => src.Category.Description))
           .ReverseMap();
        CreateMap<Cart, CartViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductCounter, opt => opt.MapFrom(src => src.CountItems))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));
        CreateMap<CartItem, CartItemViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
            .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.Product.Id))
            .ForPath(dest => dest.Product.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForPath(dest => dest.Product.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForPath(dest => dest.Product.Description, opt => opt.MapFrom(src => src.Product.Description))
            .ForPath(dest => dest.Product.FileName, opt => opt.MapFrom(src => src.Product.ImageData))
            .ForPath(dest => dest.Product.Quantity, opt => opt.MapFrom(src => src.Product.Quantity));
        CreateMap<Category, CategoryViewModel>().ReverseMap();
    }
}
