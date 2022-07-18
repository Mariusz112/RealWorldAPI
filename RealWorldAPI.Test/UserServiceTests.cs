using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using RealWorldApp.BAL.Services;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;
using RealWorldApp.DAL.Repositories;

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

            //Mock<UserManager<User>> userManager = GetMockUserManager();
            //userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);


            //to jest ca³kowicie to zmiany
            Mock<UserManager<User>> userManager = GetMockUserManager();
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var userService = new UserService(null, mockMapper.Object, null, null);
            //ACT
            var AddedUser = await userService.AddUser(user); ;
            //ASSERT
            Assert.IsNotNull(AddedUser);
            mockMapper.Verify(x => x.Map<UserResponse>(It.IsAny<User>()), Times.Once());
            Assert.That(AddedUser.User.Username, Is.EqualTo(expected.User.Username));

        }
    }
}