
using Model;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGroupSupply
    {
        Task<IResult> AddUserGroup(int Userİd, int groupId);
        Task<IResult> AddGroupAdmin(Account account, int groupId);
        Task<IResult> Create(UserGroup entity);
        IDataResult<UserGroup> Get(int id);
        IDataResult<List<UserGroup>> GetAll();
        Task<IResult> Update(int id, UserGroup entity);
    }
}
