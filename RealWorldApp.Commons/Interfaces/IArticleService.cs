using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Interfaces
{
    public interface IArticleService 
    {
        Task<ArticleAddContainer> AddArticle(ArticleAdd request);
        Task<ArticleResponse> GetArticles(string favorited, string author, int limit, int offset, string currentUserId);
        Task<ArticleAddContainer> GetArticle(string currentUserId, string title, int id);
        Task<List<ArticleAdd>> ArticleListToAUP(List<Articles> articles, string currentUserId);
        Task<List<ArticleAdd>> GetArticlesFeed(int limit, int offset, string? name);
    }
}
