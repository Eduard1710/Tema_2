using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/grades")]
    public class GradeController : ControllerBase
    {
        private GradeService gradeService { get; set; }
        private UserService userService { get; set; }

        public GradeController(GradeService gradeService, UserService userService)
        {
            this.gradeService = gradeService;
            this.userService = userService;
        }
        [HttpPost("/add-grade")]
        public IActionResult AddGrade(GradeAddDto payload)
        {
            var result = gradeService.AddGrade(payload);

            if (result == null)
            {
                return BadRequest("Grade cannot be added");
            }

            return Ok(result);
        }

        [HttpGet("/get-grades")]
        [Authorize]
        public IActionResult UserGrades()
        {
            ClaimsPrincipal user = User;

            var result = "";

            foreach (var claim in user.Claims)
            {
                result += claim.Type + " : " + claim.Value + "\n";
            }

            if (user.IsInRole("Student"))
            {
                var userId = User.Claims.Where(c => c.Type == "userId").FirstOrDefault().Value;
                return Ok(gradeService.GetStudentGrades(userService.GetStudentIdByUserId(Int32.Parse(userId))));
            }
            if (user.IsInRole("Teacher"))
                return Ok(gradeService.GetAllGrades());
            return BadRequest("Role not found!");
        }
    }
}
