using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KretaWebApi.Controllers.Base
{
    public partial class BaseController<TEntity> : ControllerBase where TEntity : class, new()
    {
        private IRepoBase _service;

        public BaseController(IRepoBase service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(_service));
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllRecordAsync()
        {
            List<TEntity>? entitys = null;
            try
            {
                entitys = await _service.SelectAllRecordAsync<TEntity>(null);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(entitys);
        }

        [HttpGet("withqueryparameters")]
        public async Task<IActionResult> SelectAllRecordAsync([FromQuery] QueryParameters queryParameters)
        {
            List<TEntity>? entitys = null;
            try
            {
                entitys = await _service.SelectAllRecordAsync<TEntity>(queryParameters);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(entitys);
        }

        [HttpGet("getpaged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagingParameters parameters)
        {
            try
            {
                PagedList<TEntity> pagedList = await _service.GetPaged<TEntity>(parameters, null);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
                return Ok(pagedList);
            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
        }

        [HttpGet("getpagedwithqueryparameters")]
        public async Task<IActionResult> GetPaged([FromQuery] PagingParameters parameters, [FromQuery] QueryParameters queryParameters)
        {
            try
            {
                PagedList<TEntity> pagedList = await _service.GetPaged<TEntity>(parameters, queryParameters);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
                return Ok(pagedList);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
        }

        [HttpPost("savewithoutid")]
        public async Task<IActionResult> AddNewItem(TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.AddNewItem(item);
            return Ok();
        }





    }
}
