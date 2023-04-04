using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SchoolClassController : BaseController<SchoolClass>
    {
        public SchoolClassController(IRepoBase service) : base(service)
        {
        }
    }
}
