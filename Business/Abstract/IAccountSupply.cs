using Model;
using Model.DTOs.Accounts;
using Model.Results;

namespace Business.Abstract;

public interface IAccountSupply
{
    IDataResult<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<IResult> Register(RegisterRequest model);
    IEnumerable<AccountResponse> GetAll();
    AccountResponse GetById(int id);
    Task<IDataResult<AccountResponse>> Create(CreateRequest model);
    IDataResult<AccountResponse> Update(int id, UpdateRequest model);
    IResult Delete(int id);
    IDataResult<List<Account>> GetUsersWithoutGroup();
    IDataResult<List<Account>> GetUsersWithPermissions();
}
