using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Models;

public class Favorities
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }

    public List<Users> FollowerUsername { get; set; }
}
