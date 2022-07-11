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
        public string Feed { get; set; }
        public string Bio { get; set; } = string.Empty;


        //to nie wiem w sumie
        public string Image { get; set; } = string.Empty;



    }
}