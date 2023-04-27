﻿using KretaCommandLine.Model;
using KretaWebApi.Controllers.Base;
using KretaWebApi.Repos.Base;
using Microsoft.AspNetCore.Mvc;

namespace KretaWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SchoolClassController : ClassWithIdBaseController<SchoolClass>
    {
        public SchoolClassController(IIncludedRepoBase service, IRepoBase repoBaseService) : base(service, repoBaseService)
        {
        }
    }
}
