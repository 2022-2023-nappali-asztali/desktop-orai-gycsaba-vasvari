using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("included")]
        public async Task<IActionResult> SelectAllIncludedRecordAsync()
        {
            List<Student>? users = null;
            try
            {
                users = await _service.SelectAllIncludedRecordAsync<Student>();

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(users);
        }
    }
}
