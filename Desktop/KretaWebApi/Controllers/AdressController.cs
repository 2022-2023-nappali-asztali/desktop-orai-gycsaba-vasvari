using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AddressController : BaseController<Address>
    {
        public AddressController(IRepoBase service) : base(service)
        {
        }
    }
}
