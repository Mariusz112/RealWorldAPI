using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;
using RealWorldApp.DAL.Repositories;
using RealWorldApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealWorldApp.BAL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IArticleRepositorie articleRepositorie;
        private readonly UserManager<User> _userManager;
        private readonly ICommentRepositorie _commentRepositorie;

        public CommentService(ILogger<UserService> logger, IMapper mapper, AuthenticationSettings authenticationSettings, IArticleRepositorie articleRepositorie, UserManager<User> userManager, ICommentRepositorie commentRepositorie)
        {
            _logger = logger;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
            this.articleRepositorie = articleRepositorie;
            _userManager = userManager;
            _commentRepositorie = commentRepositorie;
        }


       public async Task<CommentToArticlePack> AddComment(CommentToArticlePack request, string title, int id, string CurrentUserId)
        {
            Articles article = await articleRepositorie.GetArticleFromSlug(title,id);
            User user = await _userManager.FindByIdAsync(CurrentUserId);

            request.Comment.author = new AuthorToList
            {
                Bio = user.Bio,
                Username = user.UserName,
                Following = false,
                Image = user.Image
            };


            Comments comments = new Comments
            {
                Author = user,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Comment = request.Comment.Body,
            };

            await _commentRepositorie.AddComment(comments, title, id);
                
            var commentToPack = _mapper.Map<CommentToArticle>(comments);
            CommentToArticlePack CommentToPackage = new CommentToArticlePack() { Comment = commentToPack };

            return CommentToPackage;
        }

        public async Task<CommentToArticlePack> GetCommets(string title, int id)
        {
            var article = await articleRepositorie.GetArticleFromSlug(title,id);

            var comments = article.Comments.ToList();
            

            var ListToView = new List<CommentToArticle>();

            //foreach(var comment in comments)
            //{
            //    var buffor = new CommentToArticle()
            //    {
            //        Body = article.Text,
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        author = new AuthorToList()
            //        {
            //            Bio = article.Author.Bio,
            //            Following = false, 
            //            Username = article.Author.UserName,
            //            Image = article.Author.Image
            //        }
            //    };
            //    ListToView.Add(buffor);
            //}
            var commentToPack = comments.Select( comment =>  _mapper.Map<CommentToArticle>(comment)).ToList();
            var result = new CommentToArticlePack();
            result.Comments = commentToPack;
            return result;
            
        }

        public async Task DeleteCommentAsync(string title, int id, int idcomment)
        {
            //var article = await articleRepositorie.GetArticleFromSlug(title, id);
            await _commentRepositorie.DeleteCommentAsync(title, id, idcomment);
        }

    }

}
