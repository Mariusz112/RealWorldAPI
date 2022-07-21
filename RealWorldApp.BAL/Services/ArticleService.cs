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
                
               

           };

            // zapisać do bazy
            await articleRepositorie.AddArticle(article);
            //po zapisaniu zwrócę container stworzony z obiektu arcicle

            ArticleAddContainer articleAddContainer = new ArticleAddContainer() { Articles = _mapper.Map<ArticleAddContainer>(article) };
            return articleAddContainer;
       }



    }
}
