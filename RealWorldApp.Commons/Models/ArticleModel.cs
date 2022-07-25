using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Models
{
    public class ArticleAddContainer
    {
        public ArticleAddContainer Articles;

        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; } 
        
        public string Body { get; set; }
        public List<string> TagList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Favorited { get; set; }
        public string FavoritesCount { get; set; }

        public string Topic { get; set; }

        public ProfileView Profile { get; set; }
    }


    public class AddUserModel {
        public ArticleAdd Article { get; set; }
    }
    public class ArticleAdd
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<string> TagList { get; set; }
       
    }

    public class ArticleUploadResponse
    {
        public ArticleAddContainer Article { get; set; }
    }

    public class ArticleResponse
    {
        public List<ArticleAddContainer> Articles { get; set; }
        public int ArticlesCount { get; set; }
    }
}