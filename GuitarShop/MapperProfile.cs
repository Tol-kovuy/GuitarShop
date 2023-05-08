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
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<AuthenticationViewModel, User>();
        CreateMap<Product, ProductViewModel>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
           .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.ImageData))
           .ForMember(dest => dest.ImageData, opt => opt.Ignore());
    }
}
