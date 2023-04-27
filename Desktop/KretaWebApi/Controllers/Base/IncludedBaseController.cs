using APIHelpersLibrary.Paged;
using KretaCommandLine.API;
using KretaCommandLine.Model;
using KretaCommandLine.Model.Abstract;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KretaWebApi.Controllers.Base
{
    public class IncludedBaseController<TEntity> : ClassWithIdBaseController<TEntity> where TEntity : ClassWithId, new()
    {
        private IIncludedRepoBase _service;

        public IncludedBaseController(IIncludedRepoBase service, IClassWithIdRepoBase includedRepoBase, IRepoBase repoBase) :base(includedRepoBase, repoBase)
        {
            _service = service ?? throw new ArgumentNullException(nameof(_service));
        }



        [HttpGet("includedwithparameters")]
        public async Task<ActionResult<List<TEntity>>> SelectAllIncludedRecordAsync([FromQuery] QueryParameters queryParameters)
        {
            List<TEntity>? entitys = null;
            try
            {
                entitys = await _service.SelectAllIncludedRecordAsync<TEntity>(queryParameters);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(entitys);
        }

        [HttpGet("includedandpaged")]
        public async Task<ActionResult<List<TEntity>>> SelectAllIncludedRecordPagedAsync([FromQuery] PagingParameters pagingParameters)
        {
            PagedList<TEntity> pagedList = await _service.SelectAllIncludedRecordPagedAsync<TEntity>(pagingParameters, null);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
            return Ok(pagedList);
        }

        [HttpGet("includedandpagedwithparameters")]
        public async Task<ActionResult<List<Student>>> SelectAllIncludedRecordPagedAsync([FromQuery] PagingParameters pagingParameters, [FromQuery] QueryParameters queryParameters)
        {
            PagedList<TEntity> pagedList = await _service.SelectAllIncludedRecordPagedAsync<TEntity>(pagingParameters, queryParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
            return Ok(pagedList);
        }
    }
}
