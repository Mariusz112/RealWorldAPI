using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Models;

public class Tags
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Tag { get; set; }
}
