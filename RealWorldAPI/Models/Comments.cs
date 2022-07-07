using System.ComponentModel.DataAnnotations;

namespace RealWorldAPI.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }

        public string Login { get; set; }
    }
}