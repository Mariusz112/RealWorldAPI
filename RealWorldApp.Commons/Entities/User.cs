using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Entities;

public class User : IdentityUser
{
    public string Bio { get; set; } = string.Empty;
    public string? Image { get; set; }
    public List<User> FollowedUsers { get; set; } = new List<User>();
    public List<Articles> LikedArticle { get; set; } = new List<Articles>();


}