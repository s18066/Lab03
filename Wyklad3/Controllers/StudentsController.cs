using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wyklad3.DAL;
using Wyklad3.Models;

namespace Wyklad3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService service)
        {
            _dbService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery]string orderBy)
        {
            if (orderBy == "lastname")
            {
                return Ok((await _dbService.GetStudents()).OrderBy(s => s.LastName));
            }

            return Ok(await _dbService.GetStudents());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute]string id)
        {
            var foundStudent = await _dbService.GetStudent(id);
            if (foundStudent is null)
            {
                return NotFound("Student was not found");
            }

            return Ok(foundStudent);
        }
        
        [HttpGet("{id}/enrollments")]
        public async Task<IActionResult> GetStudentEnrolments([FromRoute]string id)
        {
            var foundEnrollments = await _dbService.GetStudentEnrollments(id);
            if (foundEnrollments is null)
            {
                return NotFound("Student was not found");
            }

            return Ok(foundEnrollments);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            student.IndexNumber=$"s{new Random().Next(1, 20000)}";
            //...
            return Ok(student); //JSON
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent(int studentId)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int studentId)
        {
            return Ok("Usuwanie dokończone");
        }
    }
}