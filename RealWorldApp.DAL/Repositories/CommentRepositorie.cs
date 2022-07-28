using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealWorldApp.DAL.Repositories.Interfaces;
using RealWorldApp.Commons.Entities;

namespace RealWorldApp.DAL.Repositories
{

    public class CommentRepositorie : ICommentRepositorie
    {
        private readonly ApplicationDbContext _context;

        public async Task AddComment(Comments comments)
        {
            await _context.Comment.AddAsync(comments);
            await _context.SaveChangesAsync();
        }

        public Task<Articles> GetArticleFromSlug(string title, int id)
        {
            throw new NotImplementedException();
        }
    }
}
