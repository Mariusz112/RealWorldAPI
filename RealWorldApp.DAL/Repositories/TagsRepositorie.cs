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
    public class TagsRepositorie : ITagsRepositorie
    {
        private ApplicationDbContext _context;
        public TagsRepositorie(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Tags>> GetPopularTags()
        {
            return await _context.Tag
                .Include(x => x.Articles)
                .OrderByDescending(x => x.Articles.Count)
                .ToListAsync();

        }
        public async Task AddTag(string tagName)
        {
            var tag = new Tags()
            {
                Tag = tagName
            };

            _context.Tag.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<Tags> GetTagByName(string tagName)
        {
            return await _context.Tag.Where(x => x.Tag == tagName).FirstOrDefaultAsync();
        }
    }
}
