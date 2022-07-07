using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;


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
