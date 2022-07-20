using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using RealWorldApp.BAL;
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


            //string hashPassword = string.Empty;


            Mock<UserManager<User>> userManager = GetMockUserManager();
            userManager.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(It.IsAny<User>());
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

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


            //string hashPassword = string.Empty;


            Mock<UserManager<User>> userManager = GetMockUserManager();
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);


            var userService = new UserService(mockLogger.Object, mockMapper.Object, null, userManager.Object);
            //ACT
            var AddedUser = await userService.AddUser(user);
            //ASSERT
            Assert.IsNotNull(AddedUser);
            mockMapper.Verify(x => x.Map<UserResponse>(It.IsAny<User>()), Times.Once());
            Assert.That(AddedUser.User.Username, !Is.EqualTo(expected.User.Username));

        }


        [Test]
        public async Task GenerateJWT_WithCorrectData_Test()
        {
            UserLogin userLogin = new UserLogin
            {
                Email = "test@test.com",
                Password = "test!@4",
            };
            User user = new User
            {
                Email = "test@test.com",
                PasswordHash = "test!@4",
            };


            AuthenticationSettings authenticationSettings = new AuthenticationSettings()
            {
                JwtKey = "PRIVATE_KEY_DONT_SHARE",
                JwtExpireDays = 5,
                JwtIssuer = "http://localhost:47765"
            };

            Mock<UserManager<User>> userManager = GetMockUserManager();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManager.Setup(x => x.CheckPasswordAsync(user, userLogin.Password)).ReturnsAsync(true);

            var userService = new UserService(null, null, authenticationSettings, userManager.Object);



            var jwt = await userService.GenerateJwt(userLogin.Email, userLogin.Password);

            Assert.IsNotNull(jwt);


        }

        [Test]
        public async Task GenerateJWT_WithInCorrectEmail_Test()
        {
            UserLogin userLogin = new UserLogin
            {
                Email = "test@test.com",
                Password = "test!@4",
            };
            User user = new User
            {
                Email = "test1@test1.com",
                PasswordHash = "test!@4",
            };


            AuthenticationSettings authenticationSettings = new AuthenticationSettings()
            {
                JwtKey = "PRIVATE_KEY_DONT_SHARE",
                JwtExpireDays = 5,
                JwtIssuer = "http://localhost:47765"
            };

            Mock<UserManager<User>> userManager = GetMockUserManager();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManager.Setup(x => x.CheckPasswordAsync(user, userLogin.Password)).ReturnsAsync(true);

            Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object, null, null, userManager.Object);

            Assert.ThrowsAsync(Is.TypeOf<BadRequestException>()
                .And.Message.EqualTo("Invalid username or password"), 
                async delegate { await userService.GenerateJwt("test@test.com", "test"); });
        }

        [Test]
        public async Task GenerateJWT_WithInCorrectPass_Test()
        {
            UserLogin userLogin = new UserLogin
            {
                Email = "test@test.com",
                Password = "test!@4",
            };
            User user = new User
            {
                Email = "test@test.com",
                PasswordHash = "43536t34test!@4",
            };


            AuthenticationSettings authenticationSettings = new AuthenticationSettings()
            {
                JwtKey = "PRIVATE_KEY_DONT_SHARE",
                JwtExpireDays = 5,
                JwtIssuer = "http://localhost:47765"
            };

            Mock<UserManager<User>> userManager = GetMockUserManager();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManager.Setup(x => x.CheckPasswordAsync(user, userLogin.Password)).ReturnsAsync(false);

            Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object, null, null, userManager.Object);

            Assert.ThrowsAsync(Is.TypeOf<BadRequestException>()
                .And.Message.EqualTo("Invalid username or password"),
                async delegate { await userService.GenerateJwt("test@test.com", "test"); });
        }
    }
}
