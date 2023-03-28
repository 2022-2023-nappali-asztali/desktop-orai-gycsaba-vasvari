﻿using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class AddressController : BaseController<Address>
    {
        public AddressController(IRepoBase service) : base(service)
        {
        }
    }
}