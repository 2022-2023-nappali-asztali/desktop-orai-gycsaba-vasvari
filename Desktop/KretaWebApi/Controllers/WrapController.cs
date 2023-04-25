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
            result = await _service.GetNumberOfStudentPerClass();
            return Ok(result);
        }        
    }
}
