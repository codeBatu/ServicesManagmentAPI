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

        public async Task<IResult> CanActive(int id, bool permission)
        {
            var user = await _context.GroupAccounts.FindAsync(id);
            user.CanActive = permission;
            return await saveChanges();
        }

        public async Task<IResult> CanCreate(int id, bool permission)
        {
            var user = await _context.GroupAccounts.FindAsync(id);
            user.CanCreate = true;
            return await saveChanges();
        }

        public async Task<IResult> CanGetAll(int id, bool permission)
        {
            var user = await _context.GroupAccounts.FindAsync(id);
            user.CanGetAll = true;
            return await saveChanges();
        }

        public async Task<IResult> CanInActive(int id, bool permission)
        {
            var user = await _context.GroupAccounts.FindAsync(id);
            user.CanInActive = true;
            return await saveChanges();
        }

        public async Task<IResult> CanRemove(int id, bool permission)
        {
            var user = await _context.GroupAccounts.FindAsync(id);
            user.CanRemove = true;
            return await saveChanges();
        }

        public async Task<IResult> CanRestart(int id, bool permission)
        {
            var user = _context.GroupAccounts.Find(id);
            user.CanRestart = true;
            return await saveChanges();

        }

        public async Task<IResult> CanUpdate(int id, bool permission)
        {
            var user = await _context.GroupAccounts.FindAsync(id);
            user.CanUpdate = true;
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

        public async Task<IResult> Create(GroupAccount groupAccount)
        {
            _context.GroupAccounts.Add(groupAccount);
            var result = await _context.SaveChangesAsync();

            if (result < 1)
            {
                return new ErrorResult("Hesap kaydedilemedi!.");
            }
            return new SuccessResult("Hesap başarıyla kaydedildi.");
        }

        public async Task<GroupAccount> GetById(int id)
        {
            return await _context.GroupAccounts.FindAsync(id);
        }

        //
        public async Task<IResult> Update(GroupAccount groupAccount)
        {
            _context.GroupAccounts.Update(groupAccount);
            return await saveChanges();
        }
      
    }
}
