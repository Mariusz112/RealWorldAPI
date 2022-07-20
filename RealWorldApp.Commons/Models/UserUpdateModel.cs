using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Models
{
    public class UserUpdateModelContainer
    {
        public UserUpdateModel User { get; set; }
    }
    public class UserUpdateModel
    {
        public string Bio { get; set; }
        public string Email { get; set; }
        public string? Image { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        
    }
}
