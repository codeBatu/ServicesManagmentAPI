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

namespace Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly SmartPulseServiceManagerDbContext _context;

        public GroupRepository(SmartPulseServiceManagerDbContext context)
        {
            _context = context;
        }
        private async Task<Account> findUserById(int id)
        {var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
               throw new Exception("Kullanıcı bulunamadı.");
            }
            return user;
        }
        private async Task<UserGroup> findGroupById(int id)
        {
            var user = await _context.UserGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("Grup bulunamadı.");
            }
            return user;
        }
        private async Task saveChanges(string dbSaveName)
        {
            var content = await _context.SaveChangesAsync();
            if(content ==1)
            {
                throw new Exception($"{dbSaveName} :değişiklik eklenemedi.");
            }
        }
        public async Task<IResult> AddGroupAdmin(Account account, int groupId)
        {
            var user =await findUserById(account.Id);
           
            
            var resultGroup =await findGroupById(groupId);
          
            user.Role = Role.GroupAdmin;
            await saveChanges("Group Admin Ekleme");
            return new SuccessResult("Kullanıcı gruba eklendi.");
        }


        public async Task<IResult> AddUserGroup(
           int Userİd, int groupId)
        {
            var user = await findUserById(Userİd);
          
            var resultGroup = await findGroupById(groupId);
        
            
          
            user.UserGroupId = resultGroup.Id;
        
            await saveChanges("Group User Ekleme");
            return new SuccessResult("Kullanıcı gruba eklendi.");
            
        }

        public async Task<IDataResult<int>> Create(UserGroup entity)
        {
            var result = await _context.UserGroups.FirstOrDefaultAsync(x => x.GroupName == entity.GroupName);
            if (result is not null)
            {
                return new ErrorDataResult<int>("Aynı isimde grup sistemde kayıtlı.");
            }

            _context!.UserGroups.Add(entity);
            var saveResponseCode = await _context.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorDataResult<int>("Grup kaydedilemedi!");

            }
            return new SuccessDataResult<int>(entity.Id,"Grup başarıyla kaydedildi.");
        }
        /// <summary>
        /// ServiceName göre servisi tablosundaki veriyi getirir
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IDataResult<UserGroup> GetGroup(string name)
        {
            var result = _context?.UserGroups.Include(s => s.Accounts).SingleOrDefault(t => t.GroupName == name);
            if (result is null) return new ErrorDataResult<UserGroup>("Bu isimde bir group bulunamadı!");
          
            return new SuccessDataResult<UserGroup>(result);
        }

        
        public  IDataResult<UserGroup> Get(int id)
        {
            var result = _context.UserGroups.Include(s => s.Accounts).FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                return new ErrorDataResult<UserGroup>("Grup bulunamadı.");
            }
            return new SuccessDataResult<UserGroup>( result);
        }

        public IDataResult<List<UserGroup>> GetAll()
        {
            var list = _context!.UserGroups.Include(s => s.Accounts).ToList();
            if (list is null)
            {
                return new ErrorDataResult<List<UserGroup>>("Grup bulunamadı.");
            }

            return new SuccessDataResult<List<UserGroup>>(list);
        }

        public async Task<IResult> Update(int id, UserGroup entity)
        {
            var result = await findGroupById(id);
            result.GroupName = entity.GroupName;
            if (result.GroupName == entity.GroupName)
            {
                return new ErrorResult("Grup aynı değiştirilemedi.");
            }
            _context.Update(entity);
            await saveChanges("Group Güncelleme");
            return new SuccessResult(" Başarıyla güncellendi.");
        }

        Task<IResult> IGenericRepository<UserGroup, int>.Create(UserGroup entity)
        {
            throw new NotImplementedException();
        }
    }
}
