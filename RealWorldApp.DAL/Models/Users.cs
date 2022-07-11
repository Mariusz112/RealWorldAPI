using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Models;

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