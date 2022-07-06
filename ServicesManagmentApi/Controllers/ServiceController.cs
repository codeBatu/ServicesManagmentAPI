using Business.Abstract;
using Business.Helpers.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs;
using ServicesManagmentApi.Controllers;

namespace ServiceManagerWepApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class serviceController : BaseController
    {
        private readonly IServiceSupply? _serviceManager;
        private readonly IAuthorizationUserSupply? _authManager;


        public serviceController(IServiceSupply? serviceManager, IAuthorizationUserSupply? authManager)
        {
            _serviceManager = serviceManager;
            _authManager = authManager;
        }

        [HttpPost("createService")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDTO createService)
        {
            if (Account.Role == Role.User)
            {
                var userResult = await _authManager.GetById(Account.Id);
                if (!(bool)userResult.Data.CanCreate)
                {
                    return Unauthorized();
                }
            }

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
        
        [HttpPut("updateService")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceDTO updateService)
        {
            if (Account.Role == Role.User)
            {
                var userResult = await _authManager.GetById(Account.Id);
                if (!(bool)userResult.Data.CanUpdate)
                {
                    return Unauthorized();
                }
            }

            var service = new ServiceTable{ ServiceName = updateService.ServiceName, Version = updateService.Version };

            var result = await _serviceManager!.Update(id, service);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpDelete("deleteService")]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (Account.Role == Role.User)
            {
                var userResult = await _authManager.GetById(Account.Id);
                if (!(bool)userResult.Data.CanRemove)
                {
                    return Unauthorized();
                }
            }

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
            if (Account.Role == Role.User)
            {
                var userResult = await _authManager.GetById(Account.Id);
                if (!(bool)userResult.Data.CanRestart)
                {
                    return Unauthorized();
                }
            }
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
            if (Account.Role == Role.User)
            {
                var userResult = await _authManager.GetById(Account.Id);
                if (!(bool)userResult.Data.CanActive)
                {
                    return Unauthorized();
                }
            }
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
            if (Account.Role == Role.User)
            {
                var userResult = await _authManager.GetById(Account.Id);
                if (!(bool)userResult.Data.CanInActive)
                {
                    return Unauthorized();
                }
            }
            var result = await _serviceManager!.InActiveService(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getAllServices")]
        public async Task<IActionResult> GetAllService()
        {
            if (Account.Role == Role.User)
            {
                var userResult = await _authManager.GetById(Account.Id);
                if (!(bool)userResult.Data.CanGetAll)
                {
                    return Unauthorized();
                }
            }
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
