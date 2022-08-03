using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.BAL.Services;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;

namespace RealWorldAPI.Controllers
{
    [Authorize]
    [Route("api")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        [HttpPost("articles/{title}-{id}/favorite")]
        public async Task<IActionResult> FavoriteArticle([FromRoute] string title, [FromRoute] int id)
        {
            var result = await _favoriteService.FavoriteArticle(title, id, User.Identity.Name);
            return Ok(result);
        }
        [HttpDelete("articles/{title}-{id}/favorite")]
        public async Task<IActionResult> RemoveFavoriteArticle([FromRoute] string title, [FromRoute] int id)
        {
            var result = await _favoriteService.RemoveFavoriteArticle(title, id, User.Identity.Name);
            return Ok(result);
        }
    }
}
