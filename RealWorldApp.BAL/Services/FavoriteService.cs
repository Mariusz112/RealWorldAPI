using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;
using RealWorldApp.DAL.Repositories;
using RealWorldApp.DAL.Repositories.Interfaces;

namespace RealWorldApp.BAL.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFavoriteRepositorie _favoriteRepositorie;
        private readonly ILogger _logger;
        public FavoriteService(UserManager<User> userManager, IFavoriteRepositorie favoriteRepositorie, ILogger<FollowService> logger)
        {
            _userManager = userManager;
            _favoriteRepositorie = favoriteRepositorie;
            _logger = logger;
        }

        public async Task<ArticleToList> FavoriteArticle(string title, int id, string currentUser)
        {
            var loggedUser = await _userManager.FindByIdAsync(currentUser);
            if (loggedUser == null)
            {
                _logger.LogError("Cant 't find active user");
                throw new Exception("Can't find active user");

            }
            var article = await _favoriteRepositorie.FavoriteArticle(title, id, currentUser);


            var response = new ArticleToList()
            {
                Body = article.Text,
                Slug = article.Slug,
                Description = article.Description,
                UpdatedAt = article.UpdatedAt,
                CreatedAt = article.CreatedAt,
                Title = title,
                Favorited = true,
                TagList = article.Tags.Select(x => x.Tag).ToList(),
                FavoritesCount = article.Favorited.Count,

                author = new AuthorToList()
                {
                    Username = article.Author.UserName,
                    Bio = article.Author.Bio,
                    Image = article.Author.Image,
                    Following = true

                }
            };


            return response;
        }

        public async Task<ArticleToList> RemoveFavoriteArticle(string title, int id, string currentUser)
        {
            var loggedUser = await _userManager.FindByIdAsync(currentUser);
            if (loggedUser == null)
            {
                _logger.LogError("Cant 't find active user");
                throw new Exception("Can't find active user");

            }
            var article = await _favoriteRepositorie.RemoveFavoriteArticle(title, id, currentUser);


            var response = new ArticleToList()
            {
                Body = article.Text,
                Slug = article.Slug,
                Description = article.Description,
                UpdatedAt = article.UpdatedAt,
                CreatedAt = article.CreatedAt,
                Title = title,
                Favorited = true,
                TagList = article.Tags.Select(x => x.Tag).ToList(),
                FavoritesCount = article.Favorited.Count,

                author = new AuthorToList()
                {
                    Username = article.Author.UserName,
                    Bio = article.Author.Bio,
                    Image = article.Author.Image,
                    Following = true

                }
            };


            return response;
        }
    }
}
