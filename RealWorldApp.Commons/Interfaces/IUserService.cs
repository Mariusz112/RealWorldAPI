using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Commons.Models;
using System.Security.Claims;

namespace RealWorldApp.Commons.Interfaces
{
    public interface IUserService
    {
        Task<string> GenerateJwt(string email, string password);
        Task<UserResponseContainer> AddUser(UserRegister request);
        Task<List<ViewUserModel>> GetUsers();
        Task<UserResponseContainer> GetUserByEmail(string Email);
        Task<UserResponseContainer> GetMyInfo(ClaimsPrincipal claims);
        Task<UserResponseContainer> UpdateUser(string id, UserUpdateModel request);
        Task<ViewUserModel> GetUserById(string Id);
        Task<UserViewContainer> GetProfile(string Username);
        Task<UserViewContainer> LoadProfile(string username, string id);
    }
}