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

        public async Task<IResult> CanActive(int id, bool permission)
        {
            return await _user.CanActive(id, permission);

        }

        public async Task<IResult> CanCreate(int id, bool permission)
        {
            return await _user.CanCreate(id, permission);
        }

        public async Task<IResult> CanGetAll(int id, bool permission)
        {
            return await _user.CanGetAll(id, permission);
        }

        public async Task<IResult> CanInActive(int id, bool permission)
        {
            return await _user.CanInActive(id, permission);
        }

        public async Task<IResult> CanRemove(int id, bool permission)
        {
            return await _user.CanRemove(id, permission);
        }

        public async Task<IResult> CanRestart(int id, bool permission)
        {
            return await _user.CanRestart(id, permission);
        }

        public async Task<IResult> CanUpdate(int id, bool permission)
        {
            return await _user.CanUpdate(id, permission);
        }

        public async Task<IDataResult<GroupAccount>> GetById(int id)
        {
            var account = await _user.GetById(id);
            if (account is null)
            {
                return new ErrorDataResult<GroupAccount>("Hesap bulunamadı.");
            }
            return new SuccessDataResult<GroupAccount>(account);
        }
    }
}
