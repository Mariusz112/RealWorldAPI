using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;


namespace RealWorldAPI.Models
{
    public class Users 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}