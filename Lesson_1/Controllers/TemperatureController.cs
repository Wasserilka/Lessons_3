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
        public IActionResult Create([FromQuery] string date, [FromQuery] int temperature)
        {
            try
            {
                _holder.Add(date, temperature);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            try
            {
                return Ok(_holder.Get(dateFrom, dateTo));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok("welcome");
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string date, [FromQuery] int temperature)
        {
            try
            {
                _holder.Update(date, temperature);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string date)
        {
            try
            {
                _holder.Delete(date);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }
    }
}
