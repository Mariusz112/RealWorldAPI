using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Entities;

public class Favorities
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }

    public List<User> FollowerUsername { get; set; }
}
