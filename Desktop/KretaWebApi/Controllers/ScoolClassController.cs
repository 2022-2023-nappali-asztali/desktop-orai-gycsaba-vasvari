using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SchoolClassController : IncludedBaseController<SchoolClass>
    {
        private ISchoolClassRepoBase _service;

        public SchoolClassController(ISchoolClassRepoBase service, IIncludedRepoBase includedService, IClassWithIdRepoBase classWithIdRepoBase, IRepoBase repoBaseService)
            : base(includedService, classWithIdRepoBase, repoBaseService)
        {
            _service = service;
        }



        [HttpGet("included")]
        public async Task<ActionResult<List<SchoolClass>>> SelectAllIncludedRecordAsync()
        {
            List<SchoolClass>? entitys = null;
            try
            {
                entitys = await _service.SelectAllIncludedRecordAsync<SchoolClass>(null);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(entitys);
        }
    }
}
