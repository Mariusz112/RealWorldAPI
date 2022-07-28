using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealWorldApp.Commons.Entities;

public class Comments
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    //to jest dodane migracja
    public DateTime UpdateAt { get; set; }

    public string Username { get; set; }
    public User Author { get; set; } = null!;
    public Articles Articles { get; set; }
}