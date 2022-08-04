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
using RealWorldApp.DAL.Repositories;

namespace RealWorldApp.BAL.Services
{


    public class ArticleService : IArticleService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IArticleRepositorie articleRepositorie;
        private readonly UserManager<User> _userManager;
        private readonly ICommentRepositorie _commentRepositorie;
        private readonly ITagsRepositorie _tagsRepositorie;
        private readonly IFavoriteRepositorie _favoriteRepositorie;
        private readonly IUserRepositorie _userRepositorie;

        public ArticleService(ILogger<UserService> logger, IMapper mapper, AuthenticationSettings authenticationSettings, IArticleRepositorie articleRepositorie, UserManager<User> userManager, ITagsRepositorie tagsRepositorie, IFavoriteRepositorie favoriteRepositorie, IUserRepositorie userRepositorie)
        {
            _logger = logger;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
            this.articleRepositorie = articleRepositorie;
            _userManager = userManager;
            _commentRepositorie = _commentRepositorie;
            _tagsRepositorie = tagsRepositorie;
            _favoriteRepositorie = favoriteRepositorie;
            _userRepositorie = userRepositorie;
        }


        public async Task<ArticleToListPack> AddArticle(ArticleAdd request, string CurrentUserId)
        {
            var tags = await CheckTagsAsync(request.TagList);
            Articles article = new Articles()
            {

                Title = request.Title,
                Text = request.Body,
                Tags = tags,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Description = request.Description,
                Author = await _userManager.FindByIdAsync(CurrentUserId),

            };

            // zapisać do bazy
            await articleRepositorie.AddArticle(article);
            //po zapisaniu zwrócę container stworzony z obiektu arcicle
            //mapper nie jest ci raczej potrzebny
            var articleAddContainer = new ArticleToList()
            {
                Slug = article.Slug,
                Title = article.Title,
                Description = article.Description,
                Body = article.Text,
                //TagList = article.Tag.Select(x => x.UserName)
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                Favorited = false, //todo
                FavoritesCount = 0, //todo
                author = new AuthorToList()
                {
                    Bio = article.Author.Bio,
                    Following = false, //2do
                    Username = article.Author.UserName,
                    Image = article.Author.Image
                }

            };
            var pack = new ArticleToListPack()
            {
                Article = articleAddContainer
            };
            return pack;
        }

        public async Task<ArticleListView> GetArticles(string favorited, string author, int limit, int offset, string currentUserId, string tags)
        {
            var user = await _userManager.FindByIdAsync(currentUserId);
            var articlelist = new List<ArticleToList>();
            List<Articles> articles;
            if (author != null)
            {
                articles = await articleRepositorie.GetArticlesFromAuthor(author, limit, offset);
            }
            else if (tags != null) //do zmiany na isnullorempty
            {
                articles = await articleRepositorie.GetArticlesByTags(tags, limit, offset);
            }
            else if (!string.IsNullOrEmpty(favorited))
            {
                articles = await _favoriteRepositorie.GetFavoritedArticles(currentUserId, limit, offset);
            }
            else
            {
                articles = await articleRepositorie.GetArticlesGlobal(limit, offset);
            }


            //if author null then pick all of them
            var result = new ArticleListView();
            foreach (var article in articles)
            {
                var buffor = new ArticleToList()
                {
                    Slug = article.Slug,
                    Title = article.Title,
                    Description = article.Description,
                    Body = article.Text,
                    TagList = article.Tags.Select(x => x.Tag).ToList(),
                    CreatedAt = article.CreatedAt,
                    UpdatedAt = article.UpdatedAt,
                    Favorited = user.LikedArticle.Contains(article),
                    FavoritesCount = article.Favorited.Count(),
                    author = new AuthorToList()
                    {
                        Bio = article.Author.Bio,
                        Following = false,
                        Username = article.Author.UserName,
                        Image = article.Author.Image
                    }
                };

                articlelist.Add(buffor);
            }
                
            result.Articles = articlelist;
            result.ArticleCount = 10; //todo
            return result;
        }



        /*
            public async Task<List<ArticleListView>> GetArticlesFeed(int limit, int offset, string author)
            {
                var articles = await articleRepositorie.GetNewArticleFeed(author, limit);
                var result = articles.Select(art => new ArticleListView() 
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
        */





        public Task<List<ArticleAdd>> ArticleListToAUP(List<Articles> articles, string currentUserId)
        {
            throw new NotImplementedException();
        }



        public async Task<ArticleToListPack> GetArticle(string currentUserId, string title, int id)
        {

            var article = await articleRepositorie.GetArticleFromSlug(title, id);

            var buffor = new ArticleToList()
            {
                Slug = article.Slug,
                Title = article.Title,
                Description = article.Description,
                Body = article.Text,
                //TagList = article.Tag.Select(x => x.UserName)
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                Favorited = false, //todo
                FavoritesCount = 0, //todo
                author = new AuthorToList()
                {
                    Bio = article.Author.Bio,
                    Following = false, //2do
                    Username = article.Author.UserName,
                    Image = article.Author.Image
                }
            };

            var pack = new ArticleToListPack()
            {
                Article = buffor
            };
            return pack;
        }


        public async Task DeleteArticleAsync(string title, int id)
        {
            var article = await articleRepositorie.GetArticleFromSlug(title, id);
            await articleRepositorie.DeleteArticleAsync(article);
        }

        public async Task<ArticleToListPack> UpdateArticle(ArticleToListPack request, string title, int id)
        {

            var article = await articleRepositorie.GetArticleFromSlug(title, id);

            article.Title = request.Article.Title;
            article.Description = request.Article.Description;
            article.Text = request.Article.Body;
            //article.Tag = request.TagList
            article.Slug = $"{request.Article.Title}-{id}";

            await articleRepositorie.UpdateArticle(article);

            request.Article.Slug = article.Slug;
            return request;
        }

        public async Task<List<Tags>> CheckTagsAsync(List<string> tagsNames)
        {
            var tagsInDb = await _tagsRepositorie.GetPopularTags();

            foreach (var tag in tagsNames)
            {
                if (tagsInDb.FirstOrDefault<Tags>(x => x.Tag == tag) == null)
                {
                    await _tagsRepositorie.AddTag(tag);
                }
            }

            var tagList = new List<Tags>();
            Tags buff;

            foreach (var tag in tagsNames)
            {
                buff = await _tagsRepositorie.GetTagByName(tag);
                tagList.Add(buff);
            }

            return tagList;
        }



        public async Task<ArticleListView> GetArticlesFeed(int limit, int offset, string currentUserId)
        {
            var articlelist = new List<ArticleToList>();
            var user = await _userManager.FindByIdAsync(currentUserId);

            var articles = await articleRepositorie.GetArticleFeed(limit, offset, currentUserId);

            //if author null then pick all of them
            var result = new ArticleListView();
            foreach (var article in articles)
            {
                var buffor = new ArticleToList()
                {
                    Slug = article.Slug,
                    Title = article.Title,
                    Description = article.Description,
                    Body = article.Text,
                    TagList = article.Tags.Select(x => x.Tag).ToList(),
                    CreatedAt = article.CreatedAt,
                    UpdatedAt = article.UpdatedAt,
                    Favorited = user.LikedArticle.Contains(article),
                    FavoritesCount = article.Favorited.Count(),
                    author = new AuthorToList()
                    {
                        Bio = article.Author.Bio,
                        Following = false,
                        Username = article.Author.UserName,
                        Image = article.Author.Image
                    }
                };

                articlelist.Add(buffor);
            }

            result.Articles = articlelist;
            result.ArticleCount = 10; //todo
            return result;
        }
    }
    
}
