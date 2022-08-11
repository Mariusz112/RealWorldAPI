using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using RealWorldApp.BAL.Services;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;
using RealWorldApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealWorldAPI.Test
{
    public class GetArticlesTests
    {
        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task GetArticlesTests_WithCorrectData_ReturnArticleUploadResponse()
        {
            var currentUserId = "test";
            var title = "title";
            var id = 1;

            var article = new Articles()
            {
                Title = title,
                Text = "test"
            };

            var user = new User()
            {
                UserName = "test",
                Email = "test",
            };











        }

    }
}
