using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private Mock<PasswordHasher<User>> GetMockPasswordHasher()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<PasswordHasher<User>>(
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

            
            string hashPassword = string.Empty;

           
            Mock<IPasswordHasher<User>> passwordHasher = new Mock<IPasswordHasher<User>>();
            passwordHasher.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(hashPassword);


            Mock<IUserRepositorie> mockRepostiory = new Mock<IUserRepositorie>();
            mockRepostiory.Setup(x => x.AddUser(It.IsAny<User>())).Returns(Task.CompletedTask);

            var userService = new UserService(mockRepostiory.Object, mockMapper.Object, passwordHasher.Object, null);
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


            string hashPassword = string.Empty;


            Mock<IPasswordHasher<User>> passwordHasher = new Mock<IPasswordHasher<User>>();
            passwordHasher.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(hashPassword);


            Mock<IUserRepositorie> mockRepostiory = new Mock<IUserRepositorie>();
            mockRepostiory.Setup(x => x.AddUser(It.IsAny<User>())).Returns(Task.CompletedTask);

            var userService = new UserService(mockRepostiory.Object, mockMapper.Object, passwordHasher.Object, null);
            //ACT
            var AddedUser = await userService.AddUser(user);
            //ASSERT
            Assert.IsNotNull(AddedUser);
            mockMapper.Verify(x => x.Map<UserResponse>(It.IsAny<User>()), Times.Once());
            Assert.That(AddedUser.User.Username, !Is.EqualTo(expected.User.Username));

        }
    }
}