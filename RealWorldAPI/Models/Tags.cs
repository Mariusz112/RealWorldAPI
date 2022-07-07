using System.ComponentModel.DataAnnotations;

namespace RealWorldAPI.Models
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}
