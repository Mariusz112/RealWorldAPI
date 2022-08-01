using Microsoft.EntityFrameworkCore;

using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;
using RealWorldApp.DAL.Repositories.Interfaces;


namespace RealWorldApp.DAL.Repositories
{
    public class ArticleRepositorie : IArticleRepositorie
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepositorie(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<User> GetUserById(string Id)
        {
            return await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task AddArticle(Articles article)
        {
            await _context.Article.AddAsync(article);
            await _context.SaveChangesAsync();
            
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Articles>> GetNewArticles( string author, int limit)
        {
            var articles = new List<Articles>();
            if (author != null)
            {
                articles = await _context.Article
                    .Include(u => u.Author)
                    .Where(u => u.Author.UserName == author)
                    .Take(limit)
                    .OrderByDescending(u => u.CreatedAt)
                    .ToListAsync();
            }
            else
            {
                articles = await _context.Article
                    .Include(u => u.Author)
                    .Take(limit)
                    .OrderByDescending(u => u.CreatedAt)
                    .ToListAsync();
            }
            return articles;
        }

        public Task<List<Articles>> GetNewArticles(string favorited, string author, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleResponse> GetArticlesFeed(int limit, int offset, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Articles>> GetArticlesFeed(string currentUserId, int limit)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Articles>> GetNewArticleFeed(string author, int limit)
        {


            var allArticles = await _context.Article
                .Include(u => u.Author)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            var articles = new List<Articles>();
            foreach (var article in allArticles)
            {
                  if (articles.Count > limit)
                {
                    break;
                }
            }

            return articles;
        }
        public async Task<List<Articles>> GetArticlesFromAuthor(string author, int limit, int offset)
        {
            var allArticles = await _context.Article
                .Include(u => u.Author)
                .Where(u => u.Author.UserName == author)
                .OrderByDescending(u => u.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return allArticles;
        }

        public async Task<Articles> GetArticleFromSlug(string title, int id)
        {
            var article = await _context.Article
                .Include(u => u.Author)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.Title == title && u.Id == id);
            return article;
        }

        public async Task<List<Articles>> GetArticlesGlobal(int limit, int offset)
        {
            var listGlobal = await _context.Article
                .Include(u => u.Author)
                .OrderByDescending(u => u.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return listGlobal;
        }

        public async Task DeleteArticleAsync(Articles article)
        {
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArticle(Articles article)
        {
             _context.Article.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tags>> GetAllTagsAsync()
        {
            return await _context.Tag.ToListAsync();
        }
        

    }
}

