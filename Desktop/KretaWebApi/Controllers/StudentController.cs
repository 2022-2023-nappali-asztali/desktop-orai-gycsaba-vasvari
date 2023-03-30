using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : BaseController<Student>
    {
        public StudentController(IRepoBase service) : base(service)
        {
        }
    }
}
