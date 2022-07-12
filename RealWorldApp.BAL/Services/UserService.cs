using RealWorldApp.BAL.Models;
using RealWorldApp.BAL.Services.Interfaces;
using RealWorldApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public Task AddUser(UserRegister request)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateJwt(UserLogin model)
        {
            throw new NotImplementedException();
        }

        public Task<ViewUserModel> GetUserByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<ViewUserModel> GetUserById(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewUserModel>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(string id, UserUpdateModel request)
        {
            throw new NotImplementedException();
        }
    }
}
