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


            Comments comments = new Comments
            {
                Author = user,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Comment = request.PackedComment.Comment,
                Articles = article

            };
            await _commentRepositorie.AddComment(comments);
                
            var CommentToPack = _mapper.Map<CommentToArticle>(comments);
            CommentToArticlePack CommentToPackage = new CommentToArticlePack() { PackedComment = request.PackedComment };

            return CommentToPackage;
        }




    }

}
