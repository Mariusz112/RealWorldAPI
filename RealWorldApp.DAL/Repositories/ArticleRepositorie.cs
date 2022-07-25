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
            await _context.Title.AddAsync(article);
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
                articles = await _context.Title
                    .Include(u => u.Author)
                    .Where(u => u.Author.UserName == author)
                    .Take(limit)
                    .OrderByDescending(u => u.CreatedAt)
                    .ToListAsync();
            }
            else
            {
                articles = await _context.Title
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


            var allArticles = await _context.Title
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
    }
}