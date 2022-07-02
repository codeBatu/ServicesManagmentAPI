﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IResult> AddGroupAdmin(Account account, int groupId)
        {
            var result = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);
            if (result == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı.");
            }
        
            var resultGroup = await _context.UserGroups.FirstOrDefaultAsync(z => z.Id == groupId);
            if (resultGroup is null)
            {
                return new ErrorResult("Grup bulunamadı.");
            }
            result.UserGroupId = resultGroup.Id;
            resultGroup.Admin = result.FirstName + result.LastName;
            result.Role = Role.GroupAdmin;
            await _context.SaveChangesAsync();
            return new SuccessResult("Kullanıcı gruba eklendi.");
        }


        public async Task<IResult> AddUserGroup(
           int Userİd, int groupId)
        {
            var result =await  _context.Accounts.FirstOrDefaultAsync(x => x.Id == Userİd);
            if (result == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı.");
            }

            var resultGroup =await  _context.UserGroups.FirstOrDefaultAsync(z=>z.Id==groupId);
      
          if(resultGroup is null)
            {
                return new ErrorResult("Grup bulunamadı.");
            }
            result.UserGroupId = resultGroup.Id;
            resultGroup.Member = result.FirstName+ result.LastName;
            await _context.SaveChangesAsync();
            return new SuccessResult("Kullanıcı gruba eklendi.");
            
        }

        public async Task<IResult> Create(UserGroup entity)
        {
            // isme göre servis bilgisi getir
            var result =await _context.UserGroups.FirstOrDefaultAsync(x => x.GroupName != entity.GroupName);
            // eğer aynı isimde bir servis varsa error result dön
            if (result is null)
            {
                return new ErrorResult("Aynı isimde grup sistemde kayıtlı.");
            }

            _context!.UserGroups.Add(entity);
            var saveResponseCode = await _context.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorResult("Servis kaydedilemedi!");

            }
            return new SuccessResult("Servis başarıyla kaydedildi.");
        }

      

        public  IDataResult<UserGroup> Get(int id)
        {

            return new SuccessDataResult<UserGroup>( _context.UserGroups.FirstOrDefault(x => x.Id == id));
        }

        public IDataResult<List<UserGroup>> GetAll()
        {
            var list = _context!.UserGroups.Include(s => s.Accounts).ToList();
           
            return new SuccessDataResult<List<UserGroup>>(list);
        }
    }
}
