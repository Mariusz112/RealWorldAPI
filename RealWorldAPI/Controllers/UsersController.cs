using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.DAL;
using RealWorldApp.Commons.Models;
using RealWorldApp.Commons.Interfaces;
using Microsoft.Net.Http.Headers;

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
            try
            {
                UserResponseContainer user = await _userService.AddUser(model.User);


                string token = await _userService.GenerateJwt(model.User.Email, model.User.Password);

                user.User.Token = token;

                return Ok(user);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [AllowAnonymous]
        [HttpPost("users/login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginContainer model)
        {



            try
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
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

            [HttpGet("users")]
             public async Task<IActionResult> GetUsers()
             {
                 return Ok(await _userService.GetUsers());
             } 
        
        [HttpGet("Author")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            return Ok(await _userService.GetUserByEmail(Email));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _userService.GetMyInfo(User);
            if (user == null) return NotFound();


            return Ok(user);
        }
 
        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateModelContainer request)
        {
            var id = User.Identity.Name;
            await _userService.UpdateUser(id, request.User);
            return Ok();
        }

        [HttpGet("profiles/{userName}")]
        public async Task<IActionResult> LoadProfile([FromRoute] string username)
        {
            try
            {
                var result = await _userService.LoadProfile(username, User.Identity.Name);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* [HttpPost("api/articles")]
        public async Task<IActionResult> Authenticate([FromBody] jakisartykulmodel model)
        {

        } */
    }
}