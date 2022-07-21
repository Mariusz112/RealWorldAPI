using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Models
{
    public class UserRegisterContainer
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //user register to nie wiem czy powinnien tu być
        public UserRegister User { get; set; }
    }

    public class UserRegister
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}