using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;

namespace RealWorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost()]
        public async Task<IActionResult> AddArticle([FromBody] AddUserModel request)
        {
            
            var result = await _articleService.AddArticle(request.Article);
            return Ok(result);
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetArticles([FromQuery] string favorited, [FromQuery] string author, [FromQuery] int limit, [FromQuery] int offset)
        {
            var result = await _articleService.GetArticles(favorited, author, limit, offset, User.Identity.Name);
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
            var result = await _articleService.GetArticle(title, User.Identity.Name, id);
            return Ok(result);
        }







    }
}
