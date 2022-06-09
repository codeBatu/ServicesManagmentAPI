using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace ServiceManagerWepApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceManager _serviceManager;

        public ServiceController(ServiceManager? serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("createservice")]
        public async Task<IActionResult> CreateService([FromBody]ServiceTable serviceTable)
        {
            return Ok(await  _serviceManager!.CreateService(serviceTable));
        }
        [HttpPut("updateservice")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceTable serviceTable)
        {
            return Ok(await _serviceManager!.UpdateService(id, serviceTable));
        }
        [HttpDelete("deleteservice")]
        public async Task<IActionResult> DeleteService(int id)
        {
            return Ok(await _serviceManager!.DeleteService(id));
        }
        [HttpGet("restart")]
        public void RestartServices(int id)
        {
            _serviceManager!.RestartService(id);
        }
        [HttpGet("Active")]
        public void ActiveServicesById(int id)
        {
            _serviceManager!.ActiveService(id);
        }
        [HttpGet("InActive")]
        public void InActiveServicesById(int id)
        {
            _serviceManager!.InActiveService(id);
        }
    }
}
