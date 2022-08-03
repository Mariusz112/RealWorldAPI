using RealWorldApp.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Interfaces
{
    public interface IFavoriteService
    {
        Task<ArticleToList> FavoriteArticle(string title, int id, string currentUser);
        Task<ArticleToList> RemoveFavoriteArticle(string title, int id, string currentUser);
    }
}
