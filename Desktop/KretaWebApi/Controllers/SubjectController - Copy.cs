using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TeacherController : BaseController<Subject>
    {
        public TeacherController(IIncludedRepoBase service) : base(service)
        {
        }
    }
}
