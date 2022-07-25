using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Models
{
    public class UserViewContainer
    {
        public ProfileView Profile { get; set; }
    }
    public class ProfileView
    {
        public string Bio { get; set; }
        public bool Following { get; set; } = false;
        public string Image { get; set; }
        public string Username { get; set; }
    }
}
