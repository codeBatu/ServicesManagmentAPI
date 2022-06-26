using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace ServiceManagerWepApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class serviceController : ControllerBase
    {
        private readonly IServiceSupply? _serviceManager;

        public serviceController(IServiceSupply? serviceManager)
        {
            _serviceManager = serviceManager;
        }
   
     
        [HttpPut("updateService")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceTable serviceTable)
        {
            var result = await _serviceManager!.Update(id,serviceTable);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("deleteService")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _serviceManager!.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("restartService")]
        public async Task<IActionResult> RestartServices(int id)
        {
            var result = await _serviceManager!.RestartService(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
       
        [HttpPut("activeService")]
        public async Task<IActionResult> ActiveServicesById(int id)
        {
            var result = await _serviceManager!.ActiveService(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("inActiveService")]
        public async Task<IActionResult> InActiveServicesById(int id)
        {
            var result = await _serviceManager!.InActiveService(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("createService")]
        public async Task<IActionResult> CreateService([FromBody] ServiceTable serviceTable )
        {
            var result = await _serviceManager!.Create(serviceTable);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("getAllService")]
        public IActionResult GetAllService()
        {
            var result = _serviceManager!.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
