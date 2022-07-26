using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Models
{
    public class ArticleToListPack
    {
        public ArticleToList Article { get; set; }
    }
    public class ArticleListView
    {
        public List<ArticleToList> Articles { get; set; }
        public int ArticleCount { get; set; }
    }

    public class AuthorToList
    {
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public bool Following { get; set; }
    }

    public class ArticleToList
    {
        public string Slug { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string Body { get; set; }
        public List<string> TagList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Favorited { get; set; }
        public int FavoritesCount { get; set; }

        public AuthorToList author { get; set; } 
    }
}
