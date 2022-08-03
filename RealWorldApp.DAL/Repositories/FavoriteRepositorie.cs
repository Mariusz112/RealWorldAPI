using Microsoft.EntityFrameworkCore;
using RealWorldApp.Commons.Entities;
using RealWorldApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Repositories
{
    public class FavoriteRepositorie : IFavoriteRepositorie
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepositorie(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Articles> GetArticleFromSlug(string title, int id)
        {
            var article = await _context.Article
                .Include(u => u.Author)
                .Include(u => u.Comments)
                .Include(u => u.Tags)
                .FirstOrDefaultAsync(u => u.Title == title && u.Id == id);
            return article;
        }

        public async Task<List<Articles>> GetFavoritedArticles(string currentUser, int limit, int offset)
        {
            return await _context.User
                .Include(a => a.LikedArticle)
                    .ThenInclude(a => a.Author)
                .Where(a => a.Id == currentUser)
                .SelectMany(u => u.LikedArticle)
                .OrderByDescending(u => u.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Articles> FavoriteArticle(string title, int id, string currentUser)
        {
            var article = await _context.Article
                .Include(u => u.Favorited)
                .FirstOrDefaultAsync(art => art.Title == title && art.Id == id);
            var user = await _context.User.Where(usr => usr.Id == currentUser).FirstOrDefaultAsync();

            user.LikedArticle.Add(article);
            article.Favorited.Add(user);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<Articles> RemoveFavoriteArticle(string title, int id, string currentUser)
        {
            var article = await _context.Article
                .Include(u => u.Favorited)
                .FirstOrDefaultAsync(art => art.Title == title && art.Id == id);
            var user = await _context.User.Where(usr => usr.Id == currentUser).FirstOrDefaultAsync();

            user.LikedArticle.Remove(article);
            article.Favorited.Remove(user);
            await _context.SaveChangesAsync();
            return article;
        }
    }
}
