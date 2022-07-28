using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;

namespace RealWorldAPI.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class CommentController : Controller
    {

        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }






        

        [HttpPost("articles/{title}-{id}/comments")]
        public async Task<IActionResult> AddComment([FromBody] CommentToArticlePack? request, [FromRoute] string title, [FromRoute] int id)
        {

            var result = await _commentService.AddComment(request, title, id, User.Identity.Name);
            return Ok(result);
        }

        [HttpGet("articles/{title}-{id}/comments")]
        public async Task<IActionResult> GetComments([FromRoute] string title, [FromRoute] int id)
        {
            var result = await _commentService.GetCommets(title, id);
            return Ok(result);
        }
    }
}
