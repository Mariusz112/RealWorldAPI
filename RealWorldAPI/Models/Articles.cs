using System.ComponentModel.DataAnnotations;

namespace RealWorldAPI.Models
{
    public class Articles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Tags> Comments { get; set; }
        public List<Tags> Tag { get; set; }
    }
}