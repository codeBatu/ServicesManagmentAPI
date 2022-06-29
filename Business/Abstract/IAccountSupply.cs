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
    AccountResponse Update(int id, UpdateRequest model);
    void Delete(int id);
}
