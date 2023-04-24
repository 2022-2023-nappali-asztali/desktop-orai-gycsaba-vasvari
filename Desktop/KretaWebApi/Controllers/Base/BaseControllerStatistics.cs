using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KretaWebApi.Controllers.Base
{
    public partial class BaseController<TEntity> : ControllerBase where TEntity : ClassWithId, new()
    {
        [HttpGet("count")]
        public ActionResult CountOfTEntity() 
        {
            int number = _service.GetNumberOfEntity<TEntity>();
            return Ok(number);
        }
    }
}
