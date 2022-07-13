using RealWorldApp.Commons.Models;
using System.Security.Claims;

namespace RealWorldApp.Commons.Interfaces
{
    public interface IUserService
    {
        Task<List<ViewUserModel>> GetUsers();
        Task<ViewUserModel> GetUserByEmail(string Email);
        Task<ViewUserModel> GetUserById(string Id);
        Task UpdateUser(string id, UserUpdateModel request);
        Task<UserResponseContainer> AddUser(UserRegister request);
        Task<string> GenerateJwt(string email, string password);
        Task<UserResponseContainer> GetMyInfo(ClaimsPrincipal user);
    }
}