using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Model;
using Repository;

namespace ServiceManagerWepApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogSupply? logSupply;

        public LogController(ILogSupply? logSupply)
        {
           this. logSupply = logSupply;
        }
      
        
        [HttpGet("getAllLog")]
        public IActionResult GetAllService()
        {
            var result = logSupply!.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getLogById")]
        public IActionResult GetLog(int id)
        {
            var result = logSupply!.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
