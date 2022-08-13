using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWDataTracker.Application.LookUp;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWDataTracker.Api.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class LookUpController : ControllerBase
    {
        private readonly ILookUpService lookUpService;
        public LookUpController(ILookUpService lookUpService)
        {
            this.lookUpService = lookUpService;
        }

        [HttpGet]
        public IEnumerable<LookUpDto> ListLookUpByCode(string code)
        {
            return lookUpService.ListLookUpByCode(code);
        }
    }
}

