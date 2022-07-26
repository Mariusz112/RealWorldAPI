using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Repositories.Interfaces
{
    public interface IArticleRepositorie
    {
        Task AddArticle(Articles article);
        
        Task<List<Articles>> GetNewArticles(string favorited, string author, int limit);
        
        Task <List<Articles>> GetArticlesFeed(string author, int limit);
        Task <List<Articles>> GetNewArticleFeed(string currentUserId, int limit);
        Task<List<Articles>> GetArticlesFromAuthor(string author, int limit, int offset);
        Task <Articles> GetArticleFromSlug(string title, int id);
        Task<List<Articles>> GetArticlesGlobal(int limit, int offset);
    }
}
