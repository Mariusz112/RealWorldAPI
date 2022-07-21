using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealWorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

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
    }
}
