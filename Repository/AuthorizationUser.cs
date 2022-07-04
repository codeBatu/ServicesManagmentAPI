using Model;
using Model.Results;
using Repository.DbContexts;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuthorizationUser : IAuthorizationUser
    {
        private readonly SmartPulseServiceManagerDbContext _context;

        public AuthorizationUser(SmartPulseServiceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> CanActive(int id, GroupAccount groupAccount)
        {
            var user = await _context.Accounts.FindAsync(id);
            user.GroupAccount.CanActive = true;
            return await saveChanges();
        }

        public async Task<IResult> CanCreate(int id, GroupAccount groupAccount)
        {
            var user = await _context.Accounts.FindAsync(id);
            user.GroupAccount.CanCreate = true;
            return await saveChanges();
        }

        public async Task<IResult> CanGetAll(int id, GroupAccount groupAccount)
        {
            var user = await _context.Accounts.FindAsync(id);
            user.GroupAccount.CanGetAll = true;
            return await saveChanges();
        }

        public async Task<IResult> CanInActive(int id, GroupAccount groupAccount)
        {
            var user = await _context.Accounts.FindAsync(id);
            user.GroupAccount.CanInActive = true;
            return await saveChanges();
        }

        public async Task<IResult> CanRemove(int id, GroupAccount groupAccount)
        {
            var user = await _context.Accounts.FindAsync(id);
            user.GroupAccount.CanRemove = true;
            return await saveChanges();
        }

        public async Task<IResult> CanRestart(int id, GroupAccount groupAccount)
        {
            var user = _context.Accounts.Find(id);
            user.GroupAccount.CanRestart = true;
            return await saveChanges();

        }

        public async Task<IResult> CanUpdate(int id, GroupAccount groupAccount)
        {
            var user = await _context.Accounts.FindAsync(id);
            user.GroupAccount.CanUpdate = true;
            return await saveChanges();
        }
        private async Task<IResult> saveChanges()
        {
            var result =await _context.SaveChangesAsync();
            if (result < 1)
            {
                return new ErrorResult("Hesap güncellenemedi.");
            }
            return new SuccessResult("Hesap başarıyla güncellendi.");
        }
        private async Task<Account> findUserById(int id)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
            return user;
        }
    }
}
