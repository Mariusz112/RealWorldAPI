using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Models
{
    public class CommentToArticlePack
    {
        public CommentToArticle PackedComment { get; set; }
    }

    public class CommentToArticle
    {
        public string Comment { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
