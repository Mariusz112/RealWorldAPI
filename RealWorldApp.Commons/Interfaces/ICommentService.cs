﻿using RealWorldApp.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Commons.Interfaces
{
    public interface ICommentService
    {
        Task<CommentToArticlePack> AddComment(CommentToArticlePack request, string title, int id, string CurrentUserId);
        Task<CommentToArticlePack> GetCommets(string title, int id);
        Task DeleteCommentAsync(string title, int id, int idcomment);
    }
}
