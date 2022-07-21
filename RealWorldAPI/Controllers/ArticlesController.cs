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
        public async Task<IActionResult> AddArticle([FromBody] ArticleAdd request)
        {
            
            var result = await _articleService.AddArticle(request);
            return Ok(result);
        }
    }
}
