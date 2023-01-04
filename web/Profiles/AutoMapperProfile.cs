using web.Models;
using web.Entities;
using AutoMapper;

namespace web.Profiles 
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterModel, User>();
            CreateMap<User, AuthResponse>();
            CreateMap<User, AuthRequest>();
        }
    }
}