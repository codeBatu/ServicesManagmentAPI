using Business.Abstract;
using Model;
using Model.Results;
using Repository.RepositoryInterface;

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

        public async Task<IDataResult<int>> Create(UserGroup entity)
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
