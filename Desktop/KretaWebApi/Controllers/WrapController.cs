using KretaCommandLine.DTO;
using KretaCommandLine.Model;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class WrapController : ControllerBase
    {
        private IWrapRepoBase _service;

        public WrapController(IWrapRepoBase service)
        {
            _service= service;
        }

        [HttpGet("numberofstudentperclass")]
        public async Task<ActionResult<List<NumberOfStudentInClass>>> GetNumberOfStudentPerClass()
        {
            List<NumberOfStudentInClass> result = new List<NumberOfStudentInClass>();
            try
            {
                result = await _service.GetNumberOfStudentPerClass();
            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(result);
        }

        [HttpGet("schoolclasswithnostudent")]
        public async Task<ActionResult<List<SchoolClass>>> GetSchoolClassWithNoStudent()
        {
            List<SchoolClass> result = new List<SchoolClass>();
            try
            {
                result = await _service.GetSchoolClassWithNoStudent();
            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(result);
        }

        [HttpGet("teachersubjects/{teacherId}")]
        public async Task<IActionResult> GetTeacherSubjects(int teacherId)
        {
            List<Subject> result = new List<Subject>();
            try
            {
                result = await _service.GetTeacherSubjects(teacherId);
            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(result);
        }
    }
}
