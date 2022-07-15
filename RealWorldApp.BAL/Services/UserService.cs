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
namespace RealWorldApp.BAL.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepositorie _userRepositorie;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(IUserRepositorie userRepositorie, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _userRepositorie = userRepositorie;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> GenerateJwt(string email, string password)
        {
            var user = await _userRepositorie.GetUserByEmail(email);

            if (user is null)
            {
                throw new Exception("Invalid username or password");
            }

            if (_passwordHasher.Equals(password == user.PasswordHash))
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

                if (result == PasswordVerificationResult.Failed)
                {
                    throw new Exception("Invalid username or password");
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
            var user = new User()
            {
                UserName = request.Username,
                Email = request.Email
            };

            var hashPassword = _passwordHasher.HashPassword(user, request.Password);

            user.PasswordHash = hashPassword;

            await _userRepositorie.AddUser(user);

            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };

            return userContainer;
        }

        public async Task<List<ViewUserModel>> GetUsers()
        {
            var users = await _userRepositorie.GetUsers();
            return _mapper.Map<List<ViewUserModel>>(users);
        }

        public async Task<UserResponseContainer> GetUserByEmail(string Email)
        {
            var user = await _userRepositorie.GetUserByEmail(Email);
            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };

            return userContainer;
        }

        public async Task<UserResponseContainer> GetMyInfo(ClaimsPrincipal claims)
        {
            var id = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepositorie.GetUserById(id);
            
            
            string token = await GenerateJwt(user.Email,user.PasswordHash);


            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };
            userContainer.User.Token = token;
            return userContainer;
        }

        public async Task<UserResponseContainer> UpdateUser(string id, UserUpdateModel request)
        {
            var user = await _userRepositorie.GetUserById(id);
            
            user.Bio = request.Bio;
            user.Email = request.Email;
            user.Image = request.Image;
            user.Password = request.Password;
            user.UserName = request.Username;
            await _userRepositorie.SaveChangesAsync();
            UserResponseContainer userContainer = new UserResponseContainer() { User = _mapper.Map<UserResponse>(user) };
            return userContainer;

        }

        
        public async Task<ViewUserModel> GetUserById(string Id)
        {
           var user = await _userRepositorie.GetUserById(Id);
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
            var user = await _userRepositorie.GetUserByUsername(Username);
            UserViewContainer profileContainer = new UserViewContainer() { Profile = _mapper.Map<ProfileView>(user) };

            return profileContainer;

        }
    }
}