using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreTraining20230617.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreTraining20230617.WebApi.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Success(object? value = null)
        {
            return Ok(value);
        }

        public IActionResult BadRequestError(string message)
        {
            return BadRequest(new
            {
                RespCode = 400, RespDesp = message,
                RespType = EnumRespType.Warning
            });
        }

        public IActionResult InternalServerError(object? value = null)
        {
            return StatusCode(500, value);
        }

        public IActionResult InternalServerError(Exception ex)
        {
            return StatusCode(500, new
            {
                RespCode = 999, RespDesp = ex.ToString(),
                RespType = EnumRespType.Error
            });
        }
    }
}