using Business.Abstract;
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
    public class AuthorizationUserManager : IAuthorizationUserSupply
    {
        private readonly IAuthorizationUser _user;

        public AuthorizationUserManager(IAuthorizationUser user)
        {
            _user = user;
        }

        public async Task<IResult> CanActive(int id, GroupAccount groupAccount)
        {
      return  await _user.CanActive(id, groupAccount);
            
        }

        public async Task<IResult> CanCreate(int id, GroupAccount groupAccount)
        {
            return await _user.CanCreate(id,groupAccount);
        }

        public async Task<IResult> CanGetAll(int id, GroupAccount groupAccount)
        {
            return await _user.CanGetAll(id, groupAccount);
        }

        public async Task<IResult> CanInActive(int id, GroupAccount groupAccount)
        {
            return await _user.CanInActive(id, groupAccount);
        }

        public async Task<IResult> CanRemove(int id, GroupAccount groupAccount)
        {
            return await _user.CanRemove(id, groupAccount);
        }

        public async Task<IResult> CanRestart(int id, GroupAccount groupAccount)
        {
            return await _user.CanRestart(id, groupAccount);
        }

        public async Task<IResult> CanUpdate(int id, GroupAccount groupAccount)
        {
            return await _user.CanUpdate(id, groupAccount);
        }
    }
}
