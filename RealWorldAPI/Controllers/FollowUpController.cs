using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.BAL.Services;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.Commons.Models;

namespace RealWorldAPI.Controllers
{
    [Authorize]
    [Route("api")]
    public class FollowUpController : Controller
    {
        private readonly IFollowService _followService;
        public FollowUpController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost("profiles/{username}/follow")]
        public async Task<IActionResult> FollowUser([FromRoute] string username )
        {
            var result = await _followService.AddFollow(User.Identity.Name, username);
            return Ok(result);
        }

        [HttpDelete("profiles/{username}/follow")]
        public async Task<IActionResult> UnFollowUser([FromRoute] string username)
        {
            var result = await _followService.RemoveFollow(User.Identity.Name, username);
            return Ok(result);
        }

    }
}
