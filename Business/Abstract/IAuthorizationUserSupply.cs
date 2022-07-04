using Model;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthorizationUserSupply
    {
        Task<IResult> CanUpdate(int id, GroupAccount groupAccount);

        Task<IResult> CanCreate(int id, GroupAccount groupAccount);
        Task<IResult> CanRemove(int id, GroupAccount groupAccount);
        Task<IResult> CanGetAll(int id, GroupAccount groupAccount);

        Task<IResult> CanActive(int id, GroupAccount groupAccount);
        Task<IResult> CanInActive(int id, GroupAccount groupAccount);
        Task<IResult> CanRestart(int id, GroupAccount groupAccount);
    }
}
