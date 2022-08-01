using RealWorldApp.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Repositories.Interfaces
{
    public interface ITagsRepositorie
    {

        Task<List<Tags>> GetPopularTags();
        Task AddTag(string tagName);
        Task<Tags> GetTagByName(string tagName);
    }
}
