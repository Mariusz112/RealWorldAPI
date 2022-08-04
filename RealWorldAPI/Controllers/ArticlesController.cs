using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;

namespace RealWorldAPI.Controllers
{
    [Authorize]
    [Route("api")]
    //[ApiController]
    public class ArticlesController : ControllerBase
    {

        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /*      [HttpGet("user")]
              public async Task<IActionResult> GetMyInfo()
              {
                  return Ok(await _articleService.GetMyInfo(User));
            }

       */
        [HttpGet("{slug}")]
        public async Task<IActionResult> Articles(string slug)
        {
            throw new NotSupportedException();
        }

        [HttpPost("articles")]
        public async Task<IActionResult> AddArticle([FromBody] AddUserModel request)
        {
            
            var result = await _articleService.AddArticle(request.Article, User.Identity.Name);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("articles")]
        public async Task<IActionResult> GetArticles([FromQuery] string favorited, [FromQuery] string author, [FromQuery] int limit, [FromQuery] int offset, [FromQuery] string tag)
        {
            var result = await _articleService.GetArticles(favorited, author, limit, offset, User.Identity.Name, tag);
            return Ok(result);
        }

        [HttpGet("articles/feed")]
        public async Task<IActionResult> GetArticlesFeed([FromQuery] int limit, [FromQuery] int offset)
        {
            var result = await _articleService.GetArticlesFeed(limit, offset, User.Identity.Name);
            return Ok(result);
        }


        [HttpGet("articles/{title}-{id}")]
        public async Task<IActionResult> GetArticle([FromRoute] string title, [FromRoute] int id)
        {
            var result = await _articleService.GetArticle(User.Identity.Name, title, id);
            return Ok(result);
        }


        [HttpDelete("articles/{title}-{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] string title, [FromRoute] int id)
        {
            await _articleService.DeleteArticleAsync(title, id);
            return Ok();
        }


        [HttpPut("articles/{title}-{id}")]
        public async Task<IActionResult> UpdateArticle([FromBody] ArticleToListPack request, [FromRoute] string title, [FromRoute] int id)
        {
            var result = await _articleService.UpdateArticle(request, title, id);
            return Ok(result);
        }


    }
}
