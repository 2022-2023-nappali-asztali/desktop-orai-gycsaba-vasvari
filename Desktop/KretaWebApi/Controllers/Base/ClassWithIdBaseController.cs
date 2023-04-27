using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers.Base
{
    public class ClassWithIdBaseController<TEntity> : BaseController<TEntity> where TEntity : ClassWithId, new()
    {
        private IClassWithIdRepoBase _service;

        public ClassWithIdBaseController(IClassWithIdRepoBase service, IRepoBase repoBase) : base(repoBase)
        {
            _service = service ?? throw new ArgumentNullException(nameof(_service));
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

    }
}
