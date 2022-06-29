using Microsoft.EntityFrameworkCore;
using Model;
using Model.Results;
using Repository.DbContexts;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class AccountRepository : IAccountRepository
{
    private readonly SmartPulseServiceManagerContext? _context;

    public AccountRepository(SmartPulseServiceManagerContext? context)
    {
        _context = context;
    }

    public bool CheckIfEmailExists(string email)
    {
       return _context.Accounts.Any(x => x.Email == email);
    }

    public async Task<IResult> Create(Account entity)
    {
        _context.Accounts.Add(entity);
        var result = await _context.SaveChangesAsync();

        if (result < 1)
        {
            return new ErrorResult("Hesap kaydedilemedi!.");
        }
        return new SuccessResult("Hesap başarıyla kaydedildi.");
    }

    public IDataResult<Account> Get(int id)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Account>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Account GetByMail(string email)
    {
        return _context.Accounts.SingleOrDefault(x => x.Email == email);
    }

    public IResult Update(Account account)
    {
        _context.Update(account);
        var result = _context.SaveChanges();
        if (result < 1)
        {
            return new ErrorResult("Hesap güncellenemedi.");
        }
        return new SuccessResult("Hesap başarıyla güncellendi.");
    }

    public bool IsFirstAccount()
    {
        return _context.Accounts.Count() == 0;
    }

    public bool IsEmailRegistered(string email)
    {
        return _context.Accounts.Any(x => x.Email == email);
    }
}
