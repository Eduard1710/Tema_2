using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private StudentService studentService { get; set; }


        public StudentsController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet("/get-all")]
        [Authorize(Roles = "Student")]
        public ActionResult<List<Student>> GetAll()
        {
            var results = studentService.GetAll();

            return Ok(results);
        }

        [HttpGet("/get/{studentId}")]
        [Authorize(Roles = "Student")]
        public ActionResult<Student> GetById(int studentId)
        {
            var result = studentService.GetById(studentId);

            if (result == null)
            {
                return BadRequest("Student not fount");
            }

            return Ok(result);
        }

        [HttpPatch("edit-name")]
        [Authorize(Roles = "Student")]
        public ActionResult<bool> GetById([FromBody] StudentUpdateDto studentUpdateModel)
        {
            var result = studentService.EditName(studentUpdateModel);

            if (!result)
            {
                return BadRequest("Student could not be updated.");
            }

            return result;
        }

        [HttpPost("grades-by-course")]
        [Authorize(Roles = "Student")]
        public ActionResult<GradesByStudent> Get_CourseGrades_ByStudentId([FromBody] StudentGradesRequest request)
        {
            var result = studentService.GetGradesById(request.StudentId, request.CourseType);
            return Ok(result);
        }

        [HttpGet("{classId}/class-students")]
        [Authorize(Roles = "Student")]
        public IActionResult GetClassStudents([FromRoute] int classId)
        {
            var results = studentService.GetClassStudents(classId);

            return Ok(results);
        }

        [HttpGet("grouped-students")]
        [Authorize(Roles = "Student")]
        public IActionResult GetGroupedStudents()
        {
            var results = studentService.GetGroupedStudents();

            return Ok(results);
        }
    }
}
