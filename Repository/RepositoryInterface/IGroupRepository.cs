using Model;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IGroupRepository :IGenericRepository<UserGroup, int>
    {
        Task<IResult> AddUserGroup(int Userİd,int groupId);
        Task<IResult> AddGroupAdmin(Account account, int groupId);
    
        
        


    }
}
