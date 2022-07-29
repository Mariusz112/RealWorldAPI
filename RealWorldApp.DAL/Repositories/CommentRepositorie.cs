using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealWorldApp.DAL.Repositories.Interfaces;
using RealWorldApp.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace RealWorldApp.DAL.Repositories
{

    public class CommentRepositorie : ICommentRepositorie
    {
        private readonly ApplicationDbContext _context;
        public CommentRepositorie(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddComment(Comments comments, string title, int id)
        {
            var article = await _context.Article.FirstOrDefaultAsync(x => x.Id == id && x.Title == title);
            article.Comments.Add(comments);
            _context.Article.Update(article);
            
            await _context.SaveChangesAsync();
        }





        public async Task DeleteCommentAsync(string title, int id, int idcomment)
        {
            var article = await _context.Article
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Title == title && x.Id == id);

            var comments = await _context.Comment
                .FirstOrDefaultAsync(x => x.Id == idcomment);

            article.Comments.Remove(comments);

            _context.Comment.Remove(comments);
            _context.Article.Update(article);
            await _context.SaveChangesAsync();
        }
    }
}
