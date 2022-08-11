using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace RealWorldApp.Commons.Entities;

public class Articles 
{

    public int Id { get; set; }
    public string? Title { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Slug { get; set; }
    public string Description { get; set; }

    public User Author { get; set; } = new User();
    public List<Tags> Tags { get; set; } = new List<Tags>();
    public List<Comments> Comments { get; set; } = new List<Comments>();
    public List<User> Favorited { get; set; } = new List<User>();
}