using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson_1.Controllers
{
    [Route("api/temperature")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public TemperatureController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            _holder.Add(date, temperature);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            return Ok(_holder.Get(dateFrom, dateTo));
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            _holder.Update(date, temperature);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            _holder.Delete(dateFrom, dateTo);
            return Ok();
        }
    }
}
