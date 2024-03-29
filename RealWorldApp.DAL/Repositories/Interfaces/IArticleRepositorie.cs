﻿using RealWorldApp.Commons.Entities;
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
       
    
        //Task <List<Articles>> GetArticlesFeed(string author, int limit);
        Task<List<Articles>> GetNewArticles(string author, int limit, int offset);
        Task<List<Articles>> GetArticlesFromAuthor(string author, int limit, int offset);
        Task <Articles> GetArticleFromSlug(string title, int id);
        Task<List<Articles>> GetArticlesGlobal(int limit, int offset);
        Task UpdateArticle(Articles article);
        Task DeleteArticleAsync(Articles article);
        Task<List<Tags>> GetAllTagsAsync();
        Task<List<Articles>> GetArticlesByTags(string tag, int limit, int offset);
        //Task<List<Articles>> GetArticlesFeed(int limit, int offset, string currentUserId);
        // Task<List<Articles>> GetFeed(User user);
        Task<List<Articles>> GetArticleFeed(int limit, int offset, string currentUserId);

    }
}
