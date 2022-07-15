using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.DAL;
using RealWorldApp.Commons.Models;
using RealWorldApp.Commons.Interfaces;
namespace RealWorldAPI.Controllers
{
    [Authorize]
    [Route("api")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("users")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterContainer model)
        {
            UserResponseContainer user = await _userService.AddUser(model.User);


            string token = await _userService.GenerateJwt(model.User.Email, model.User.Password);

            user.User.Token = token;

            return Ok(user);
        }//return response


        [AllowAnonymous]
        [HttpPost("users/login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginContainer model)
        {
            var user = await _userService.GetUserByEmail(model.User.Email);
            string token = await _userService.GenerateJwt(model.User.Email, model.User.Password);


            var response = new UserResponseContainer()
            {
                User = new UserResponse
                {
                    Token = token,
                    Bio = user.User.Bio,
                    Email = user.User.Email,
                    Image = string.Empty,
                    Username = user.User.Username,
                }
            };

            return Ok(response);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("Username")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            return Ok(await _userService.GetUserByEmail(Email));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetMyInfo()
        {
            return Ok(await _userService.GetMyInfo(User));
        }

        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateModelContainer request)
        {
            var id = User.Identity.Name;
            await _userService.UpdateUser(id, request.User);
            return Ok();
        }

        [HttpGet("profiles/{Username}")]
        public async Task<IActionResult> GetProfile([FromRoute] string Username)
        {
            return Ok(await _userService.GetProfile(Username));
        }
    }
}