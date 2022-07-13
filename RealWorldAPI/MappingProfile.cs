using AutoMapper;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;

namespace RealWorldAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ViewUserModel>();
            CreateMap<User, UserResponse>();
        }
    }
}