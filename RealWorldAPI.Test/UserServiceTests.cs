using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using RealWorldApp.BAL.Services;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;
using RealWorldApp.DAL.Repositories;
using RealWorldApp.DAL.Repositories.Interfaces;

namespace RealWorldAPI.Test
{
    public class UserServiceTests
    {
        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        //ARRANGE
        //ACT
        //ASSERT
        [Test]
        public async Task UserRegister_WithCorrectData_Test()
        {
            //ARRANGE
            UserRegister user = new UserRegister()
            {
                Email = "Test1",
                Password = "Test2",
                Username = "Test3"
            };

            UserResponse response = new UserResponse()
            {
                Email = "Test1",
                Username = "Test3"
            };
            UserResponseContainer expected = new UserResponseContainer()
            {
                User = new UserResponse
                {
                    Email = "Test1",
                    Username = "Test3"
                }
            };

            //mockup
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserResponse>(It.IsAny<User>())).Returns(response);

            Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();

            
            string hashPassword = string.Empty;


            Mock<UserManager<User>> userManager = GetMockUserManager();
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);


            //Mock<UserManager<User>> userManager = GetMockUserManager();
            //mockRepostiory.Setup(x => x.AddUser(It.IsAny<User>())).Returns(Task.CompletedTask);

            var userService = new UserService(mockLogger.Object,mockMapper.Object, null, userManager.Object);
            //ACT
            var AddedUser = await userService.AddUser(user); 
            //ASSERT
            Assert.IsNotNull(AddedUser);
            mockMapper.Verify(x => x.Map<UserResponse>(It.IsAny<User>()), Times.Once());
            Assert.That(AddedUser.User.Username, Is.EqualTo(expected.User.Username));

        }
        [Test]
        public async Task UserRegister_WithInCorrectData_Test()
        {
            //ARRANGE
            UserRegister user = new UserRegister()
            {
                Email = "Test1",
                Password = "Test2",
                Username = "Test3"
            };

            UserResponse response = new UserResponse()
            {
                Email = "Test1",
                Username = "Test3"
            };
            UserResponseContainer expected = new UserResponseContainer()
            {
                User = new UserResponse
                {
                    Email = "Tesf43f3t1",
                    Username = "rvfrewvf3wf"
                }
            };

            //mockup
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserResponse>(It.IsAny<User>())).Returns(response);

            Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();


            string hashPassword = string.Empty;


            Mock<UserManager<User>> userManager = GetMockUserManager();
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);


            //Mock<UserManager<User>> userManager = GetMockUserManager();
            //mockRepostiory.Setup(x => x.AddUser(It.IsAny<User>())).Returns(Task.CompletedTask);

            var userService = new UserService(mockLogger.Object, mockMapper.Object, null, userManager.Object);
            //ACT
            var AddedUser = await userService.AddUser(user);
            //ASSERT
            Assert.IsNotNull(AddedUser);
            mockMapper.Verify(x => x.Map<UserResponse>(It.IsAny<User>()), Times.Once());
            Assert.That(AddedUser.User.Username, !Is.EqualTo(expected.User.Username));

        }
    }
}