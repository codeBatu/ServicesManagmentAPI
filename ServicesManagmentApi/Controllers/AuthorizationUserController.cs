using Business.Abstract;
using Business.Helpers.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs.Role;

namespace ServicesManagmentApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationUserController : ControllerBase
    {
        private readonly IAuthorizationUserSupply _authorizationUser;

        public AuthorizationUserController(IAuthorizationUserSupply authorizationUser)
        {
            _authorizationUser = authorizationUser;
        }
        [Authorize(Role.Admin,Role.GroupAdmin)]
        [HttpPut("createService")]
        public async Task<ActionResult<GroupAccount>> Create(int id, AuthorizationUserRoleDto model)
        {
            var dto = new GroupAccount() { CanCreate=model.CanCreate};
       var result =   await _authorizationUser.CanCreate(id, dto);
          
            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanActiveService")]
        public async Task<ActionResult<GroupAccount>> CanActive(int id, GroupAccount model)
        {
            var result = await _authorizationUser.CanActive(id, model);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanGetAllService")]
        public async Task<ActionResult<GroupAccount>> CanGetAll(int id, GroupAccount model)
        {
            var result = await _authorizationUser.CanGetAll(id, model);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanInActiveService")]
        public async Task<ActionResult<GroupAccount>> CanInActive(int id, GroupAccount model)
        {
            var result = await _authorizationUser.CanInActive(id, model);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanRemoveService")]
        public async Task<ActionResult<GroupAccount>> CanRemove(int id, GroupAccount model)
        {
            var result = await _authorizationUser.CanRemove(id, model);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanRestartService")]
        public async Task<ActionResult<GroupAccount>> CanRestart(int id, GroupAccount model)
        {
            var result = await _authorizationUser.CanRestart(id, model);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanUpdateService")]
        public async Task<ActionResult<GroupAccount>> CanUpdate(int id, GroupAccount model)
        {
            var result = await _authorizationUser.CanUpdate(id, model);

            return Ok(result);
        }

    }
}
