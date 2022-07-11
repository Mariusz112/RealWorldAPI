﻿using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;


namespace RealWorldAPI.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }

        public string Login { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public string Username { get; set; }
        public Users Author { get; set; } = null!;
    }
}