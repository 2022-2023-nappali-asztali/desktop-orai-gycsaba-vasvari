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
        private IIncludedRepoBase _service;

        public BaseController(IIncludedRepoBase service)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(long id)
        {
            TEntity entity = null;
            try
            {
                entity = await _service.GetBy<TEntity>(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(entity);
        }

        [HttpPost()]
        public async Task<IActionResult> Insert(TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await Save(item);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] TEntity item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await Save(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                APICallState callState = await _service.Delete<TEntity>(id);
                if (callState == APICallState.Success)
                {
                    return Ok("Törlés sikerült.");
                }
                else
                {
                    return BadRequest("Törlés sikertelen.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region Private method
        private async Task<IActionResult> Save(TEntity item)
        {
            if (item is object)
            {
                try
                {
                    //await _userRepo.Save(user);
                    APICallState callState = await _service.Save(item);
                    if (callState == APICallState.Success)
                    {
                        return Ok("Mentés sikerült.");
                    }
                    else
                    {
                        return BadRequest("Mentés sikertelen.");
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
                return BadRequest("A menteni kívánt elelm nem létezik.");
        }
        #endregion
    }
}
