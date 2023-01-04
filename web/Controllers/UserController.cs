using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Models;
using web.Services;
using web.Entities;

namespace web.Controllers 
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model) 
        {
            userService.Register(model);
            return Ok(new { message = "Registration was successful!" });
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthRequest model) 
        {
            return Ok(userService.Authenticate(model));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(userService.GetUsers());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(userService.GetUser(id));
        }

        [Authorize]
        [HttpDelete("remove")]
        public IActionResult Delete(int id) 
        {
            userService.RemoveUser(id);
            return Ok(new { message = "User was successfully deleted!" });
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(User user)
        {
            userService.UpdateUser(user);
            return Ok(new { message = "User was successfully updated!" });
        }
    }
}
