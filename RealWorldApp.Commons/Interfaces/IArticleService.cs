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
        Task<ArticleToListPack> AddArticle(ArticleAdd request, string CurrentUserId);
        Task<ArticleListView> GetArticles(string favorited, string author, int limit, int offset, string currentUserId, string tags);
        Task<ArticleToListPack> GetArticle(string currentUserId, string title, int id);
        Task<List<ArticleAdd>> ArticleListToAUP(List<Articles> articles, string currentUserId);
        //  Task<List<ArticleAdd>> GetArticlesFeed(int limit, int offset, string author);
        Task<ArticleToListPack> UpdateArticle(ArticleToListPack request, string title, int id);
        Task DeleteArticleAsync(string title, int id);
        Task<List<Tags>> CheckTagsAsync(List<string> tagsNames);
        Task<ArticleListView> GetArticlesFeed(int limit, int offset, string currentUserId);

    }
}
