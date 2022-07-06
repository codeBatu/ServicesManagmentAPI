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
    public class AuthorizationUserController : BaseController
    {
        private readonly IAuthorizationUserSupply _authorizationUser;
        private readonly IAccountSupply _accountManager;

        public AuthorizationUserController(IAuthorizationUserSupply authorizationUser, IAccountSupply accountManager)
        {
            _authorizationUser = authorizationUser;
            _accountManager = accountManager;
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("createService")]
        public async Task<ActionResult<GroupAccount>> Create(int id, bool canCreate)
        {
            if (Account.Role == Role.GroupAdmin)
            {
                var user = _accountManager.GetById(id);
                if (user.UserGroupId != Account.UserGroupId)
                {
                    return BadRequest("Grup adminleri yalnızca kendi grubundakilere yetki verebilir.");
                }
            }

            var result = await _authorizationUser.CanCreate(id, canCreate);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanActiveService")]
        public async Task<ActionResult<GroupAccount>> CanActive(int id, bool canActive)
        {
            if (Account.Role == Role.GroupAdmin)
            {
                var user = _accountManager.GetById(id);
                if (user.UserGroupId != Account.UserGroupId)
                {
                    return BadRequest("Grup adminleri yalnızca kendi grubundakilere yetki verebilir.");
                }
            }
            var result = await _authorizationUser.CanActive(id, canActive);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanGetAllService")]
        public async Task<ActionResult<GroupAccount>> CanGetAll(int id, bool canGetAll)
        {
            if (Account.Role == Role.GroupAdmin)
            {
                var user = _accountManager.GetById(id);
                if (user.UserGroupId != Account.UserGroupId)
                {
                    return BadRequest("Grup adminleri yalnızca kendi grubundakilere yetki verebilir.");
                }
            }
            var result = await _authorizationUser.CanGetAll(id, canGetAll);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanInActiveService")]
        public async Task<ActionResult<GroupAccount>> CanInActive(int id, bool canInactive)
        {
            if (Account.Role == Role.GroupAdmin)
            {
                var user = _accountManager.GetById(id);
                if (user.UserGroupId != Account.UserGroupId)
                {
                    return BadRequest("Grup adminleri yalnızca kendi grubundakilere yetki verebilir.");
                }
            }
            var result = await _authorizationUser.CanInActive(id, canInactive);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanRemoveService")]
        public async Task<ActionResult<GroupAccount>> CanRemove(int id, bool canRemove)
        {
            if (Account.Role == Role.GroupAdmin)
            {
                var user = _accountManager.GetById(id);
                if (user.UserGroupId != Account.UserGroupId)
                {
                    return BadRequest("Grup adminleri yalnızca kendi grubundakilere yetki verebilir.");
                }
            }
            var result = await _authorizationUser.CanRemove(id, canRemove);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanRestartService")]
        public async Task<ActionResult<GroupAccount>> CanRestart(int id, bool canRestart)
        {
            if (Account.Role == Role.GroupAdmin)
            {
                var user = _accountManager.GetById(id);
                if (user.UserGroupId != Account.UserGroupId)
                {
                    return BadRequest("Grup adminleri yalnızca kendi grubundakilere yetki verebilir.");
                }
            }
            var result = await _authorizationUser.CanRestart(id, canRestart);

            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPut("CanUpdateService")]
        public async Task<ActionResult<GroupAccount>> CanUpdate(int id, bool canUpdate)
        {
            var result = await _authorizationUser.CanUpdate(id, canUpdate);

            return Ok(result);
        }

    }
}
