using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AddressController : ClassWithIdBaseController<Address>
    {
        public AddressController(IIncludedRepoBase service, IRepoBase repoBaseService) : base(service, repoBaseService)
        {
        }
    }
}
