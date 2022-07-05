using Model;
using Model.DTOs.Accounts;
using Model.Results;

namespace Repository.RepositoryInterface;

public interface IAccountRepository : IGenericRepository<Account, int>
{
    IResult Update(Account account);

    Account GetByMail(string email);
    bool CheckIfEmailExists(string email);
    bool IsFirstAccount();
    bool IsEmailRegistered(string email);
    IResult Delete(int id);
    IDataResult<List<UserWithPermissions>> GetUsersWithPermissions();
    IDataResult<List<Account>> GetUsersWithoutGroup();
}
