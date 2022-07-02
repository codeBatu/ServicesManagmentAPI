using Business.Abstract;
using Business.Helpers.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs;
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
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPost("createService")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDTO createService)
        {
            // map dto to serviceTable entity
            var service = new ServiceTable
            {
                ServiceName = createService.ServiceName,
                ServiceStatus = createService.ServiceStatus,
                Version = createService.Version
            };

            var result = await _serviceManager!.Create(service);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("updateService")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceDTO updateService)
        {
            var service = new ServiceTable{ ServiceName = updateService.ServiceName, Version = updateService.Version };

            var result = await _serviceManager!.Update(id, service);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
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
        [Authorize(Role.Admin, Role.GroupAdmin)]
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
        [Authorize(Role.Admin, Role.GroupAdmin)]
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
        [Authorize(Role.Admin, Role.GroupAdmin)]
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

        [HttpGet("getAllServices")]
        public IActionResult GetAllService()
        {
            var result = _serviceManager!.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetServiceById")]
        public IActionResult GetById(int id)
        {
            var result = _serviceManager!.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpGet("GetServiceByName")]
        public IActionResult GetByName(string name)
        {
            var result = _serviceManager!.GetService(name);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
