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

public class AccountManager : IAccountSupply
{
    private readonly IAccountRepository _accountDal;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly IRoleRepository _roleDal;

    public AccountManager(
        IAccountRepository accountDal,
        IJwtUtils jwtUtils,
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        IRoleRepository roleDal)
    {
        _accountDal = accountDal;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _appSettings = appSettings.Value;
        _roleDal = roleDal;
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

        // convert string to enum
        RoleEnum role;
        Enum.TryParse(model.RoleEnum, out role);

        // save account
        await _accountDal.Create(account);
        await addRole(account.Id, role);

        var response = _mapper.Map<AccountResponse>(account);
        return new SuccessDataResult<AccountResponse>(response);
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AccountResponse> GetAll()
    {
        throw new NotImplementedException();
    }

    public AccountResponse GetById(int id)
    {
        throw new NotImplementedException();
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
        account.Created = DateTime.UtcNow;

        // hash password
        account.PasswordHash = BCrypt.HashPassword(model.Password);

        // first registered account is an admin
        var isFirstAccount = _accountDal.IsFirstAccount();

        // save to db
        var result = await _accountDal.Create(account);

        if (isFirstAccount)
        {
            await addRole(account.Id, RoleEnum.Admin);
        }

        return result;
    }

    public AccountResponse Update(int id, UpdateRequest model)
    {
        throw new NotImplementedException();
    }

    private async Task addRole(int accountId, RoleEnum role)
    {
        await _roleDal.Create(new() { AccountId = accountId, RoleValue = role });
    }
}
