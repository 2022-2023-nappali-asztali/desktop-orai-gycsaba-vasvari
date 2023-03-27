﻿using KretaCommandLine.API;
using KretaCommandLine.Model.Abstract;
using KretaWebApi.Repos;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers.Base
{
    public class BaseController<TEntity> : ControllerBase where TEntity : ClassWithId, new()
    {
        private IRepoBase _service;

        public BaseController(IRepoBase service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(_service));
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllRecordAsync()
        {
            List<TEntity>? users = null;
            try
            {
                users = await _service.SelectAllRecordAsync<TEntity>();

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(users);
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

        [HttpDelete()]
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
