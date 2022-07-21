using RealWorldApp.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Interfaces
{
    public interface IArticleService 
    {
        Task<ArticleAddContainer> AddArticle(ArticleAdd request);
    }
}
