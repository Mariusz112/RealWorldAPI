using RealWorldApp.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Interfaces
{
    public interface IFollowService
    {
        Task<UserViewContainer> AddFollow(string currentUser, string userToFollow);
        Task<UserViewContainer> RemoveFollow(string currentUser, string userToFollow);
    }
}
