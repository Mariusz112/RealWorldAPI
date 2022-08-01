using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Entities;

public class Tags
{
    
    public int Id { get; set; }
    
    public string Tag { get; set; }

    public List<Articles> Articles { get; set; } = new List<Articles>();
    
}
