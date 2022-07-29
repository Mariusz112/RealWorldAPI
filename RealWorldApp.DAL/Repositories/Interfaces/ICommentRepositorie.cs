using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.DAL.Repositories.Interfaces
{
    public interface ICommentRepositorie
    {

        Task AddComment(Comments comments, string title, int id);
        Task DeleteCommentAsync(string title, int id, int idcomment);
       


    }
}
