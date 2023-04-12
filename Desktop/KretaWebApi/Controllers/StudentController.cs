﻿using APIHelpersLibrary.Paged;
using KretaCommandLine.Model;
using KretaCommandLine.QueryParameter;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : BaseController<Student>
    {
        private IStudentRepoBase _service;

        public StudentController(IStudentRepoBase service) : base(service)
        {
            _service= service;
        }

        [HttpGet("included")]
        public async Task<ActionResult<List<Student>>> SelectAllIncludedRecordAsync()
        {
            List<Student>? students = null;
            try
            {
                students = await _service.SelectAllIncludedRecordAsync<Student>(null);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(students);
        }

        [HttpGet("includedwithparameters")]
        public async Task<ActionResult<List<Student>>> SelectAllIncludedRecordAsync(QueryParameters queryParameters)
        {
            List<Student>? students = null;
            try
            {
                students = await _service.SelectAllIncludedRecordAsync<Student>(queryParameters);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(students);
        }

        [HttpGet("includedandpaged")]
        public async Task<ActionResult<List<Student>>> SelectAllIncludedRecordPagedAsync([FromQuery] PagingParameters pagingParameters)
        {
            PagedList<Student> pagedList = await _service.SelectAllIncludedRecordPagedAsync<Student>(pagingParameters,null);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
            return Ok(pagedList);
        }

        [HttpGet("includedandpagedwithparameters")]
        public async Task<ActionResult<List<Student>>> SelectAllIncludedRecordPagedAsync([FromQuery] PagingParameters pagingParameters, QueryParameters queryParameters)
        {
            PagedList<Student> pagedList = await _service.SelectAllIncludedRecordPagedAsync<Student>(pagingParameters, queryParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
            return Ok(pagedList);
        }

        [HttpGet("byclass/{schoolClassId}")]
        public async Task<ActionResult<List<Student>>> SelectStudentOfClass(long schoolClassId)
        {
            List<Student>? students = null;
            try
            {
                students = await _service.SelectStudentOfClass<Student>(schoolClassId);

            }
            catch (Exception ex)
            {
                return BadRequest("Az adatbázis nem elérhető.");
            }
            return Ok(students);
        }
    }
}
