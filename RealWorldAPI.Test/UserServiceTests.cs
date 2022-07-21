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

        [Test]
        public async Task UpdateUser_WithCorrectData_Test() {
            string id = string.Empty;
            var request = new UserUpdateModel()
            {
                Bio = "test1",
                Email = "test1@test1.com",
                Image = "test1",
                Username = "tester1",
            };


            User userBeforeUpdate = new User
            {
                Bio = "test",
                Email = "test@test.com",
                Image = "test",
                UserName = "test",

            }; 

            UserResponse expectedUserModel = new UserResponse
            {
                Bio = "test1",
                Email = "test1@test1.com",
                Image = "test1",
                Username = "tester1",
            };

 
            Mock<UserManager<User>> mockUserManager = GetMockUserManager();
            mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(userBeforeUpdate);
            mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

         
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<UserResponse>(It.IsAny<User>())).Returns(expectedUserModel);




            //Act
            var userService = new UserService(null, mockMapper.Object, null, mockUserManager.Object);

            var updatedUserProfile = userService.UpdateUser(id,request );



            //Assert

            
            Assert.IsTrue(updatedUserProfile != null);


        }

        [Test]
        public async Task UpdateUser_WithNullUser_Test()
        {
            string id = string.Empty;
            var request = new UserUpdateModel()
            {
                Bio = "test1",
                Email = "test1@test1.com",
                Image = "test1",
                Username = "tester1",
            };

            var user = new User()
            {
                Bio = "test",
                Email = "test@test.com",
                Image = "test",
                UserName = "test",

            };



            var userUpdate = new UserResponse()
            {
                Bio = "test1",
                Email = "test1@test1.com",
                Image = "test1",
                Username = "tester1",
            };


            Mock<UserManager<User>> userManager = GetMockUserManager();
            userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(default(User));



            var mockLogger = new Mock<ILogger<UserService>>();


            //Act
            var userService = new UserService(mockLogger.Object, null, null, userManager.Object);





            //Assert


            Assert.ThrowsAsync(Is.TypeOf<BadRequestException>().And.Message.EqualTo("Bad request for user"), async delegate { await userService.UpdateUser(id, request); });


        }



    }
}
