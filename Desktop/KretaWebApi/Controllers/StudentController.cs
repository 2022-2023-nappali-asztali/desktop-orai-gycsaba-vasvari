﻿using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : BaseController<Student>
    {
        private IStudentRepoBase _service;

        public StudentController(IStudentRepoBase service) : base(service)
        {
            _service= service;
        }      

        [HttpGet("byclass/{schoolClassId}")]
        public async Task<ActionResult<List<Student>>> SelectStudentOfClass(long schoolClassId)
        {
            List<Student>? students = null;
            try
            {
                students = await _service.SelectStudentOfClass<Student>(schoolClassId);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(students);
        }
    }
}
