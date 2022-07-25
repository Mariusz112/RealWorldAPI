using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RealWorldApp.Commons.Models;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.DAL;
using RealWorldApp.Commons;
using RealWorldApp.Commons.Entities;
using RealWorldApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace RealWorldApp.BAL.Services
{


    public class ArticleService : IArticleService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IArticleRepositorie articleRepositorie;
        private readonly UserManager<User> _userManager;


        public ArticleService(ILogger<UserService> logger, IMapper mapper, AuthenticationSettings authenticationSettings, IArticleRepositorie articleRepositorie)
        {
            _logger = logger;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
            this.articleRepositorie = articleRepositorie;
            
        }

  
       public async Task<ArticleAddContainer> AddArticle(ArticleAdd request)
       {
            Articles article = new Articles()
            {

                Title = request.Title,
                Text = request.Body,
                Tag = request.TagList.Select(tag => new Tags() { Tag = tag }).ToList(),
                Favorited = new List<User>(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Description = request.Description,
                //Author = request.User,


            };

            // zapisać do bazy
            await articleRepositorie.AddArticle(article);
            //po zapisaniu zwrócę container stworzony z obiektu arcicle
            //mapper nie jest ci raczej potrzebny
            ArticleAddContainer articleAddContainer = new ArticleAddContainer() { Articles = _mapper.Map<ArticleAddContainer>(article) };
            //add articleview = profileview
            return articleAddContainer;
       }

        public async Task<List<ArticleAdd>> GetArticles(string currentUserId, int limit)
        {
            var articles = await articleRepositorie.GetNewArticleFeed(currentUserId, limit);
            var result = articles.Select(art => new ArticleAdd() 
            {
             Body = art.Text,
             Title = art.Title,
             TagList = art.Tag.Select(x => x.Tag).ToList(),
            }).ToList();
            //var pack = new ArticleResponse()
            //{
            //    Articles = articles.Select(art => new ArticleAddContainer() 
            //    { 
            //        Text = string.Empty,
            //        //TODO
                
            //    }).ToList(),
            //    ArticlesCount = limit,
            //};
            
            return result;
        }



        public async Task<List<ArticleAdd>> GetArticlesFeed(int limit, int offset, string author)
        {
            var articles = await articleRepositorie.GetNewArticleFeed(author, limit);
            var result = articles.Select(art => new ArticleAdd() 
            {
             Body = art.Text,
             Title = art.Title,
             TagList = art.Tag.Select(x => x.Tag).ToList(),
            }).ToList();
            //var pack = new ArticleResponse()
            //{
            //    Articles = articles.Select(art => new ArticleAddContainer() 
            //    { 
            //        Text = string.Empty,
            //        //TODO
                
            //    }).ToList(),
            //    ArticlesCount = limit,
            //};
            
            return result;
        }

        public Task<List<ArticleAdd>> ArticleListToAUP(List<Articles> articles, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleResponse> GetArticles(string favorited, string author, int limit, int offset, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleAddContainer> GetArticle(string currentUserId, string title, int id)
        {
            throw new NotImplementedException();
        }



        /*     public async Task<ArticleResponse> GetArticlesFeed(int limit, int offset, string currentUserId, IArticleRepositorie articleRepositorie)
             {
                 var Article = await articleRepositorie.GetArticlesFeed(currentUserId, limit);

                 var pack = new ArticleResponse()
                 {
                     Article = AddArticle(Article, currentUserId).Result,
                     articlesCount = limit
                 };

                 return pack;
          }  */
    }
}
