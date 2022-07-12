using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealWorldApp.DAL.Models;

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