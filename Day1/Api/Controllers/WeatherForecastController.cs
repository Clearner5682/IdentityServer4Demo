using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy ="WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new List<dynamic> { 
                new { Date="2020.8.22",Temperature=35},
                new { Date="2020.8.23",Temperature=36},
                new { Date="2020.8.24",Temperature=37}
            });
        }
    }
}
