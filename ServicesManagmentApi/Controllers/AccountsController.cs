using Business.Abstract;
using Business.Helpers.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs.Accounts;
using Model.Results;

namespace ServicesManagmentApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountsController : BaseController
{
    private readonly IAccountSupply _accountManager;
    private readonly IGroupSupply _groupManager;

    public AccountsController(IAccountSupply accountManager, IGroupSupply groupManager)
    {
        _accountManager = accountManager;
        _groupManager = groupManager;
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

    [Authorize(Role.Admin)]
    [HttpGet("getWithPermissions")]
    public ActionResult<IEnumerable<UserWithPermissions>> GetAllWithPermissions()
    {
        var accounts = _accountManager.GetUsersWithPermissions();
        return Ok(accounts);
    }

    [Authorize(Role.Admin)]
    [HttpGet("getWithoutGroup")]
    public ActionResult<IEnumerable<UserWithPermissions>> GetAllWithoutGroup()
    {
        var accounts = _accountManager.GetUsersWithoutGroup();
        return Ok(accounts);
    }

    [Authorize(Role.Admin, Role.GroupAdmin)]
    [HttpGet("getWithoutGroup")]
    public ActionResult<Model.Results.IResult> AddUserToGroup(int id, int groupId)
    {
        if (Account.Role == Role.GroupAdmin && !(Account.UserGroupId == groupId))
        {
            return BadRequest(new ErrorResult("group admins can only add to their group"));
        }

        var result = _groupManager.Get(groupId);
        if (!result.Success)
        {
            return BadRequest(new ErrorResult("group cannot found"));
        }

        var user = _accountManager.GetById(id);
        if (user is null)
        {
            return BadRequest(new ErrorResult("user cannot found"));
        }

        _accountManager.Update(id, new UpdateRequest { UserGroupId = groupId });
        return Ok(new SuccessResult($"user added to {result.Data.GroupName}"));

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
