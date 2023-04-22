using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private UserService userService { get; set; }

        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("/register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDto payload)
        {
            userService.Register(payload);
            return Ok();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto payload)
        {
            var jwtToken = userService.Validate(payload);

            return Ok(new { token = jwtToken });
        }

        //[HttpGet("/grades")]
        //[Authorize]
        //public ActionResult<string> UserGrades()
        //{
        //    ClaimsPrincipal user = User;
        //    if (user.IsInRole("Student"))
        //    {
        //        return Ok("Hello student!");
        //    }
        //    if (user.IsInRole("Teacher"))
        //        return Ok("Hello teacher!");
        //    return BadRequest("Role not found!");
        //}
    }
}
