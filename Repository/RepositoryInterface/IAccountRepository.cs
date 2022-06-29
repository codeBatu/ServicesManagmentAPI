using Model;
using Model.Results;

namespace Repository.RepositoryInterface;

public interface IAccountRepository : IGenericRepository<Account, int>
{
    IResult Update(Account account);

    Account GetByMail(string email);
    bool CheckIfEmailExists(string email);
    bool IsFirstAccount();
    bool IsEmailRegistered(string email)
}
