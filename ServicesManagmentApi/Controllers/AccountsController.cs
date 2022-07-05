using Business.Abstract;
using Business.Helpers.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs.Accounts;

namespace ServicesManagmentApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountsController : BaseController
{
    private readonly IAccountSupply _accountManager;

    public AccountsController(IAccountSupply accountManager)
    {
        _accountManager = accountManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        var result = await _accountManager.Register(model);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var result = _accountManager.Authenticate(model);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result.Data);
    }

    [Authorize(Role.Admin)]
    [HttpPost("create")]
    public async Task<ActionResult<AccountResponse>> Create(CreateRequest model)
    {
        var result = await _accountManager.Create(model);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        // users can delete their own account and admins can delete any account
        if (id != Account.Id && Account.Role != Role.Admin)
            return Unauthorized(new { message = "Unauthorized" });

        var result = _accountManager.Delete(id);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    // only admins can view all the users and group admins can view the users in their group
   // [Authorize(Role.Admin,Role.GroupAdmin)]
    [HttpGet]
    public ActionResult<IEnumerable<AccountResponse>> GetAll()
    {
        var accounts = _accountManager.GetAll();

        // group admins can get their group
        if (Account.Role == Role.GroupAdmin)
        {
            return Ok(accounts.Where(a => a.UserGroupId == Account.UserGroupId));
        }

        return Ok(accounts);
    }

    [AllowAnonymous]
    [HttpGet("getWithPermissions")]
    public ActionResult<IEnumerable<UserWithPermissions>> GetAllWithPermissions()
    {
        var accounts = _accountManager.GetUsersWithPermissions();
        return Ok(accounts);
    }
    
    [AllowAnonymous]
    [HttpGet("getWithoutGroup")]
    public ActionResult<IEnumerable<UserWithPermissions>> GetAllWithoutGroup()
    {
        var accounts = _accountManager.GetUsersWithoutGroup();
        return Ok(accounts);
    }

    [HttpGet("{id:int}")]
    public ActionResult<AccountResponse> GetById(int id)
    {
        // users can get their own account, admins can get any account
        if (id != Account.Id && Account.Role == Role.User)
            return Unauthorized(new { message = "Unauthorized" });

        var account = _accountManager.GetById(id);

        //  group admins can get only from their group
        if (Account.Role == Role.GroupAdmin && Account.UserGroupId != account.UserGroupId)
        {
            return Unauthorized(new { message = "Unauthorized" });
        }

        return Ok(account);
    }
    

    [HttpPut("{id:int}")]
    public ActionResult<AccountResponse> Update(int id, UpdateRequest model)
    {
        // users can update their own account and admins can update any account
        if (id != Account.Id && Account.Role == Role.User)
            return Unauthorized(new { message = "Unauthorized" });

        // only admins can update role and group
        if (Account.Role != Role.Admin)
        {
            model.Role = null;
            model.UserGroupId = 0;
        }

        if (Account.Role == Role.GroupAdmin)
        {
            var response = _accountManager.GetById(id);
            if (response.UserGroupId != Account.UserGroupId) return Unauthorized(new { message = "Unauthorized" });
        }

        var account = _accountManager.Update(id, model);
        return Ok(account);
    }
}
