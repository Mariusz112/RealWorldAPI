using Microsoft.EntityFrameworkCore;

using RealWorldApp.Commons.Entities;
using RealWorldApp.DAL.Repositories.Interfaces;

namespace RealWorldApp.DAL.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly ApplicationDbContext _context;

        public UserRepositorie(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            return await _context.Users.Where(x => x.Email == Email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(string Id)
        {
            return await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByUsername(string Username)
        {
            return await _context.Users.Where(x => x.UserName == Username).FirstOrDefaultAsync();
        }
    }
}