using Model;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IAuthorizationUser
    {
        Task<IResult> Create(GroupAccount groupAccount);
        Task<IResult> CanUpdate(int id, bool permission);

        Task<IResult> CanCreate(int id, bool permission);
        Task<IResult> CanRemove(int id, bool permission);
        Task<IResult> CanGetAll(int id, bool permission);

        Task<IResult> CanActive(int id, bool permission);
        Task<IResult> CanInActive(int id, bool permission);
        Task<IResult> CanRestart(int id, bool permission);
        Task<GroupAccount> GetById(int id);
    }
}
