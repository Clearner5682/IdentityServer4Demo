using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        public IActionResult Test1()
        {
            return Ok("Test1");
        }

        [Authorize(Policy ="Test")]
        public IActionResult Test2()
        {
            return Ok("Test2");
        }
    }
}