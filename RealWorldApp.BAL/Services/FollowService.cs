using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;
using RealWorldApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.BAL.Services
{

    public class FollowService : IFollowService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFollowRepositorie _followRepositorie;
        private readonly ILogger _logger;


        public FollowService(UserManager<User> userManager, IFollowRepositorie followRepositorie, ILogger<FollowService> logger)
        {
            _userManager = userManager;
            _followRepositorie = followRepositorie;
            _logger = logger;

        }
        public async Task<UserViewContainer> AddFollow(string currentUser, string userToFollow)
        {
            var loggedUser = await _userManager.FindByIdAsync(currentUser);
            if(loggedUser == null)
            {
                _logger.LogError("Cant 't find active user");
                throw new Exception("Can't find active user");

            }

            var NewUserToFollow = await _userManager.FindByNameAsync(userToFollow);

            if (!loggedUser.FollowedUsers.Contains(NewUserToFollow))
            {
                loggedUser.FollowedUsers.Add(NewUserToFollow);
                await _userManager.UpdateAsync(loggedUser);
            }

            var response = new UserViewContainer()
            {
                Profile = new ProfileView()
                {
                    Username = NewUserToFollow.UserName,
                    Bio = NewUserToFollow.Bio,
                    Image = NewUserToFollow.Image,
                    Following = true

                }
            };


            return response;
        }

        public async Task<UserViewContainer> RemoveFollow(string currentUser, string userToFollow)
        {
            var loggedUser = await _userManager.FindByIdAsync(currentUser);
            if (loggedUser == null)
            {
                _logger.LogError("Cant 't find active user");
                throw new Exception("Can't find active user");

            }

            var NewUserToFollow = await _userManager.FindByNameAsync(userToFollow);

            if (loggedUser.FollowedUsers.Contains(NewUserToFollow))
            {
                loggedUser.FollowedUsers.Remove(NewUserToFollow);
                await _userManager.UpdateAsync(loggedUser);
            }

            var response = new UserViewContainer()
            {
                Profile = new ProfileView()
                {
                    Username = NewUserToFollow.UserName,
                    Bio = NewUserToFollow.Bio,
                    Image = NewUserToFollow.Image,
                    Following = true

                }
            };


            return response;
        }
    }
}
