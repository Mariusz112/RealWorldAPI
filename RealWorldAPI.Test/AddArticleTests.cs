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
    public class AddArticleTests
    {
        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
        [Test]
        public async Task AddArticle_WithCorrectData_ReturnPack()
        {
            //Arrange
            var CurrentUserId = "test";
            
            var tags = new List<Tags>()
            {
                new Tags()
                {
                    Tag = "test1",

                },
                new Tags()
                {
                    Tag = "test2"
                }

            };

            var request = new ArticleAdd()
            {
                Body = "test",
                Description = "test",
                TagList = new List<string>()
                {
                    "test1", "test2"
                },
                Title = "test",

            };
            var article = new Articles()
            {
                Title = "test",
                Description="test",
                Id = 1,
                Slug = string.Empty,
                Text = "test",
                Comments = new List<Comments>(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Favorited = new List<User>(),
                Author = new User(),
                Tags = new List<Tags>()
                
            };

            var tag = new Tags()
            {
                Tag = "dummy"
            };

            User response = new User()
            {
                Email = "Test1",
                UserName = "Test3"
            };

            Mock<UserManager<User>> userManager = GetMockUserManager();
            userManager.Setup(x => x.FindByIdAsync(CurrentUserId)).ReturnsAsync(response);

            var articleRepositorie = new Mock<IArticleRepositorie>();

            var tagsRepositorie = new Mock<ITagsRepositorie>();
            tagsRepositorie.Setup(x => x.GetPopularTags()).ReturnsAsync(tags);
            tagsRepositorie.Setup(x => x.GetTagByName(It.IsAny<string>())).ReturnsAsync(tag);

            Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();

            var articleService = new ArticleService(mockLogger.Object, null, null, articleRepositorie.Object, userManager.Object, tagsRepositorie.Object, null, null);


            //Act
            var result = await articleService.AddArticle(request, CurrentUserId);

            Assert.NotNull(result);
            Assert.That(result.Article.Title, Is.EqualTo(article.Title));
        }
    }
}
