using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Results;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GroupManager : IGroupSupply
    {
        private readonly IGroupRepository groupRepository;
        public GroupManager(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<Model.Results.IResult> AddGroupAdmin(Account account, int groupId)
        {
            return await groupRepository.AddGroupAdmin(account, groupId);
        }

        public async Task<Model.Results.IResult> AddUserGroup(int Userİd, int groupId)
        {
            return await groupRepository.AddUserGroup(Userİd, groupId);
        }

        public async Task<Model.Results.IResult> Create(UserGroup entity)
        {
            return await groupRepository.Create(entity);
        }

        public IDataResult<UserGroup> Get(int id)
        {
            return groupRepository.Get(id);
        }

        public IDataResult<List<UserGroup>> GetAll()
        {
            return groupRepository.GetAll();
        }

        public async Task<Model.Results.IResult> Update(int id, UserGroup entity)
        {
            return await groupRepository.Update(id, entity);
        }
    }
}
