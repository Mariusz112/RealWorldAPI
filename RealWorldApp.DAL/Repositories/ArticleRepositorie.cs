using Microsoft.EntityFrameworkCore;

using RealWorldApp.Commons.Entities;
using RealWorldApp.DAL.Repositories.Interfaces;


namespace RealWorldApp.DAL.Repositories
{
    public class ArticleRepositorie : IArticleRepositorie
    {
        private readonly ApplicationDbContext _context;


        public async Task<User> GetUserById(string Id)
        {
            return await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

    }
}