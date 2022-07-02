using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs.Accounts;
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
    private readonly SmartPulseServiceManagerDbContext? _context;

    public AccountRepository(SmartPulseServiceManagerDbContext? context)
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
        var account = _context.Accounts.Find(id);
        if (account is null)
        {
            return new ErrorDataResult<Account>("Hesap bulunamadı!");
        }
        return new SuccessDataResult<Account>(account);
    }

    public IDataResult<List<Account>> GetAll()
    {
        var accounts = _context.Accounts.Include(s=>s.UserGroup);
        return new SuccessDataResult<List<Account>>(accounts.ToList());
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

    public IResult Delete(int id)
    {
        var result = Get(id);
        if (!result.Success)
        {
            return result;
        }
        _context.Accounts.Remove(result.Data);
        _context.SaveChanges();
        return new SuccessResult("Hesap silindi!");
    }
}
