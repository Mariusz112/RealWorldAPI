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
            

            CreateMap<Articles, ArticleAddContainer>()
                .ForMember(x=> x.Body, y=> y.MapFrom(c=> c.Text))
                .ForMember(x => x.TagList, y => y.MapFrom(c => c.Tags.Select(t=> t.Tag)))
                .ForMember(x => x.Topic, y => y.MapFrom(c => c.Description))
                .ForMember(x => x.Profile, y => y.MapFrom(c => c.Author))
                .ReverseMap();

            CreateMap<Comments, CommentToArticle>()
                .ForMember(x => x.Body, y => y.MapFrom(c => c.Comment))
                .ForMember(x => x.author, y => y.MapFrom(c => c.Author))
                .ForMember(x => x.CreatedAt, y => y.MapFrom(c => c.CreatedAt))
                .ForMember(x => x.UpdatedAt, y => y.MapFrom(c => c.UpdateAt))
                .ReverseMap();
        }
    }
}