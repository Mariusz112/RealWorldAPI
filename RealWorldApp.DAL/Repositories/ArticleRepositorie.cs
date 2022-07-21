using Microsoft.EntityFrameworkCore;

using RealWorldApp.Commons.Entities;
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

   
    }
}