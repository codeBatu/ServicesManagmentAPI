namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Helpers.Authorization;
using Microsoft.Extensions.Options;
using Model;
using Model.DTOs.Accounts;
using Repository.RepositoryInterface;
using BCrypt.Net;
using Model.Results;
using System.Collections.Generic;

public class AccountManager : IAccountSupply
{
    private readonly IAccountRepository _accountDal;
    private readonly IAuthorizationUser _groupAccountDal;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public AccountManager(
        IAccountRepository accountDal,
        IAuthorizationUser groupAccountDal,
        IJwtUtils jwtUtils,
        IMapper mapper,
        IOptions<AppSettings> appSettings
        )
    {
        _accountDal = accountDal;
        _groupAccountDal = groupAccountDal;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    public IDataResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var account = _accountDal.GetByMail(model.Email);

        // validate
        if (account == null || !BCrypt.Verify(model.Password, account.PasswordHash))
            return new ErrorDataResult<AuthenticateResponse>("Email ya da şifre hatalı");

        // authentication successful so generate jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(account);

        // save changes to db
        _accountDal.Update(account);

        // map account to response model
        var response = _mapper.Map<AuthenticateResponse>(account);
        response.JwtToken = jwtToken;
        return new SuccessDataResult<AuthenticateResponse>(response);
    }

    public async Task<IDataResult<AccountResponse>> Create(CreateRequest model)
    {
        // validate
        if (_accountDal.IsEmailRegistered(model.Email))
            return new ErrorDataResult<AccountResponse>("Email adresi kullanılıyor!");

        // map model to new account object
        var account = _mapper.Map<Account>(model);
        account.Created = DateTime.UtcNow;

        // hash password
        account.PasswordHash = BCrypt.HashPassword(model.Password);

        // save account
        await _accountDal.Create(account);
        if (account.Role == Role.User)
        {
            await _groupAccountDal.Create(new GroupAccount
            {
                AccountId = account.Id,
                CanGetAll = true,
                CanActive = false,
                CanCreate = false,
                CanInActive = false,
                CanRemove = false,
                CanRestart = false,
                CanUpdate = false
            });
        }

        var response = _mapper.Map<AccountResponse>(account);
        return new SuccessDataResult<AccountResponse>(response);
    }

    public IResult Delete(int id)
    {
        return _accountDal.Delete(id);
    }

    public IEnumerable<AccountResponse> GetAll()
    {
        var accounts = _accountDal.GetAll().Data;
        return _mapper.Map<IList<AccountResponse>>(accounts);
    }

    public AccountResponse GetById(int id)
    {
        var account = _accountDal.Get(id).Data;
        return _mapper.Map<AccountResponse>(account);
    }

    public IDataResult<List<Account>> GetUsersWithoutGroup()
    {
        return _accountDal.GetUsersWithoutGroup();
    }

    public IDataResult<List<UserWithPermissions>> GetUsersWithPermissions()
    {
        return _accountDal.GetUsersWithPermissions();
    }

    public async Task<IResult> Register(RegisterRequest model)
    {
        // validate
        if (_accountDal.CheckIfEmailExists(model.Email))
        {
            return new ErrorResult("Bu email kullanılıyor.");
        }

        // map model to new account object
        var account = _mapper.Map<Account>(model);

        // first registered account is an admin
        var isFirstAccount = _accountDal.IsFirstAccount();
        account.Role = isFirstAccount ? Role.Admin : Role.User;
        account.Created = DateTime.UtcNow;

        // hash password
        account.PasswordHash = BCrypt.HashPassword(model.Password);

        // save to db
        var result = await _accountDal.Create(account);

        return result;
    }

    public IDataResult<AccountResponse> Update(int id, UpdateRequest model)
    {
        var account = _accountDal.Get(id).Data;

        // validate
        if (account.Email != model.Email && _accountDal.CheckIfEmailExists(model.Email))
            return new ErrorDataResult<AccountResponse>($"Mail '{model.Email}' sistemde kayıtlı!");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            account.PasswordHash = BCrypt.HashPassword(model.Password);

        // copy model to account and save
        _mapper.Map(model, account);
        account.Updated = DateTime.UtcNow;
        _accountDal.Update(account);

        var response = _mapper.Map<AccountResponse>(account);
        return new SuccessDataResult<AccountResponse>(response);
    }
}
