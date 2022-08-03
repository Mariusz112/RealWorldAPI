using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RealWorldApp.Commons.Models;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.DAL;
using RealWorldApp.Commons;
using RealWorldApp.Commons.Entities;
using RealWorldApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealWorldApp.BAL.Services
{
    public class UserService : IUserService
    {

        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly UserManager<User> _userManager;

        public UserService(ILogger<UserService> logger, IMapper mapper, AuthenticationSettings authenticationSettings, UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
            _userManager = userManager;
        }

        public async Task<string> GenerateJwt(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                _logger.LogError("Invalid username or password");
                throw new BadRequestException("Invalid username or password");
            }

            if (!password.Equals(user.PasswordHash))
            {
                var result = await _userManager.CheckPasswordAsync(user, password);

                if (!result)
                {
                    _logger.LogError("Invalid username or password");
                    throw new BadRequestException("Invalid username or password");
                }
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, $"{user.Email}"),
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Name, $"{user.Id}"),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserResponseContainer> AddUser(UserRegister request)
        {
            User user = new User()
            {
                UserName = request.Username,
                Email = request.Email,
            };

            var emailResult = await _userManager.CreateAsync(user, request.Password);

            if (!emailResult.Succeeded)
            {
                _logger.LogError(", ", emailResult.Errors.Select(x => x.Description));
                throw new ArgumentException("Email already taken");
            }


/*
            if (!emailResult.Succeeded)
            {
                _logger.LogError(", ", emailResult.Errors.Select(x => x.Description));
                throw new BadRequestException(string.Join( ", ", emailResult.Errors.Select(x => x.Description)));
            }
*/
            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };

            return userContainer;
        }

        public async Task<List<ViewUserModel>> GetUsers()
        {
            var usersList = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<ViewUserModel>>(usersList);
        }

        public async Task<UserResponseContainer> GetUserByEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };

            return userContainer;
        }

        public async Task<UserResponseContainer> GetMyInfo(ClaimsPrincipal claims)
        {
            var id = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id);


            string token = await GenerateJwt(user.Email,user.PasswordHash);


            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };
            userContainer.User.Token = token;
            var userResponseContainer = new UserResponseContainer()
            {
                User = new UserResponse()
                {
                    Email = user.Email,
                    Username = user.UserName,
                    Bio = user.Bio,
                    Image = user.Image,
                    
                }
            };

            return userContainer;
        }

        public async Task<UserResponseContainer> UpdateUser(string id, UserUpdateModel request)
        {
            var user = await _userManager.FindByIdAsync(id);

            if ( user == null)
            {
                _logger.LogError("Bad request for user");
                throw new BadRequestException("Bad request for user");

            }



            user.Bio = request.Bio;
            user.Email = request.Email;
            user.Image = request.Image;
            //user.Password = request.Password;
            user.UserName = request.Username;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError(string.Join(", ", result.Errors.Select(x => x.Description)));
                throw new BadRequestException(string.Join(" ", result.Errors.Select(x => x.Description)));
            }

            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };
            return userContainer;

        }

        
        public async Task<ViewUserModel> GetUserById(string Id)
        {
           var user = await _userManager.FindByIdAsync(Id);
           var result = new ViewUserModel() { 
           Id = user.Id,  
           Email = user.Email,
           Bio = user.Bio,
           Username = user.UserName,

            };
            return result;
        }
        public async Task<UserViewContainer> GetProfile(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);

            UserViewContainer profileContainer = new UserViewContainer() { Profile = _mapper.Map<ProfileView>(user) };

            return profileContainer;

        }

        public async Task<UserViewContainer> LoadProfile(string username, string id)
        {
            User user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                _logger.LogError($"Can not find user {username}");
                throw new BadRequestException($"Can not find user {username}");
            }

            var currentUser = await _userManager.FindByIdAsync(id);
            


            UserViewContainer profileContainer = new UserViewContainer() { Profile = _mapper.Map<ProfileView>(user) };

            profileContainer.Profile.Following = currentUser.FollowedUsers.Contains(user);

            return profileContainer;
        }
    }
}