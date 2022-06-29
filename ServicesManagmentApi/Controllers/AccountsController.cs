﻿using Business.Abstract;
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

    [Authorize(RoleEnum.Admin)]
    [HttpPost]
    public async Task<ActionResult<AccountResponse>> Create(CreateRequest model)
    {
        var result = await _accountManager.Create(model);
        if(!result.Success)
        {
            return BadRequest(result.Message);
}
        return Ok(result.Data);
    }
}
