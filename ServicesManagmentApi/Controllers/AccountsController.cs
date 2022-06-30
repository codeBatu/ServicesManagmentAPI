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
        if(!result.Success)
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
        if(!result.Success)
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
        if(!result.Success)
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

    // only admins can view all the users
    [Authorize(Role.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<AccountResponse>> GetAll()
    {
        var accounts = _accountManager.GetAll();
        return Ok(accounts);
    }
}
