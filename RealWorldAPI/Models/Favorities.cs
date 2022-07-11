using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace RealWorldAPI.Models
{
    public class Favorities
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }

        public List<Users> FollowerUsername { get; set; }
    }
}
