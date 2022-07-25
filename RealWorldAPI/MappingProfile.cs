using AutoMapper;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;

namespace RealWorldApp.Commons
{ 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ViewUserModel>();
            CreateMap<User, UserResponse>();
            CreateMap<User, ProfileView>();
            CreateMap<Articles, ArticleAdd>()
                .ForMember(x=> x.Body, y=> y.MapFrom(c=> c.Text))
                .ForMember(x => x.TagList, y => y.MapFrom(c => c.Tag.Select(t=> t.Tag)))

                .ReverseMap();
        }
    }
}