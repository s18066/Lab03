﻿using System;
using System.Linq;
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

        //2. QueryString
        [HttpGet]
        public IActionResult GetStudents([FromServices]IDbService service, [FromQuery]string orderBy)
        {
            if (orderBy == "lastname")
            {
                return Ok(_dbService.GetStudents().OrderBy(s => s.LastName));
            }

            return Ok(_dbService.GetStudents());
        }

        //[FromRoute], [FromBody], [FromQuery]
        //1. URL segment
        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute]int id) //action method
        {
            if (id == 1)
            {
                return Ok("Jan");
            }

            return NotFound("Student was not found");
        }

        //3. Body - cialo zadan
        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            student.IndexNumber=$"s{new Random().Next(1, 20000)}";
            //...
            return Ok(student); //JSON
        }

        [HttpPut("{id:int")]
        public IActionResult UpdateStudent(int studentId)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id:intd}")]
        public IActionResult DeleteStudent(int studentId)
        {
            return Ok("Usuwanie dokończone");
        }
    }
}