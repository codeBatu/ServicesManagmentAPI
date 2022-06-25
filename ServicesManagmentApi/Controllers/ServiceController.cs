using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using ServiceLayer;

namespace ServiceManagerWepApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class serviceController : ControllerBase
    {
        private readonly ServicesLayer? _serviceManager;

        public serviceController(ServicesLayer? serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("createService")]
        public async Task<IActionResult> CreateService([FromBody]ServiceTable serviceTable)
        {
            return Ok(await  _serviceManager!.CreateService(serviceTable));
        }
        [HttpPut("updateService")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceTable serviceTable)
        {
            return Ok(await _serviceManager!.UpdateService(id, serviceTable));
        }
        [HttpDelete("deleteService")]
        public async Task<IActionResult> DeleteService(int id)
        {
            return Ok(await _serviceManager!.DeleteService(id));
        }
        [HttpPut("restartService")]
        public void RestartServices(int id)
        {
            _serviceManager!.RestartService(id);
        }
        [HttpPut("activeService")]
        public void ActiveServicesById(int id)
        {
            _serviceManager!.ActiveService(id);
        }
        [HttpPut("inActiveService")]
        public void InActiveServicesById(int id)
        {
            _serviceManager!.InActiveService(id);
        }
        
        [HttpGet("getAllService")]
        public List<ServiceTable> GetAllService()
        {
            return _serviceManager!.GetAllService();
        }

    }
}
