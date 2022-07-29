using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Models
{
    public class CommentToArticlePack
    {
        public CommentToArticle? Comment { get; set; }
        public List<CommentToArticle>? Comments { get; set; }
    }


    public class CommentToArticle

    {
        public int Id { get; set; }
        public string Body { get; set; } 
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AuthorToList? author { get; set; }
    }
}
