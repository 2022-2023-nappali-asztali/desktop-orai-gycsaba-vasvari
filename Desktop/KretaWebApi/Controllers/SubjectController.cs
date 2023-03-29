using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SubjectController : BaseController<Subject>
    {
        public SubjectController(IRepoBase service) : base(service)
        {
        }
    }
}
