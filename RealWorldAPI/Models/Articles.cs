using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace RealWorldAPI.Models
{
    public class Articles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public string Title { get; set; }
        public string Text { get; set; }
        public string Topic { get; set; }
        public List<Users> Username { get; set; }
        public List<Comments> Comment { get; set; }
        public List<Tags> Tag { get; set; }


    }
}