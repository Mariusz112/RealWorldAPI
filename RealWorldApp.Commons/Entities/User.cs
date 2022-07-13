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
    public string Feed { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    //to nie wiem w sumie
    public string Image { get; set; } = string.Empty;



}