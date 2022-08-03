using RealWorldApp.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Repositories.Interfaces
{
    public interface IFavoriteRepositorie
    {
        Task<Articles> GetArticleFromSlug(string title, int id);
        Task<Articles> FavoriteArticle(string title, int id, string currentUser);
        Task<Articles> RemoveFavoriteArticle(string title, int id, string currentUser);
        Task<List<Articles>> GetFavoritedArticles(string currentUser, int limit, int offset);
    }
}
