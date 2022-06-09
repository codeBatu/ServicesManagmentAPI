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

    }
}
