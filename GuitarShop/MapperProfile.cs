using AutoMapper;
using GuitarShop.BLL.Models;
using GuitarShop.DAL.Entities;
using GuitarShop.Models;

namespace GuitarShop;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap<RegistrationViewModel, User>();
        CreateMap<AuthenticationViewModel, User>();
        
    }
}
