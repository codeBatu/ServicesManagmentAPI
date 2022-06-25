using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IGenericRepository<TEntity, T>
    {
        Task<IResult> Create(TEntity entity);
        Task<IResult> Update(T id, TEntity entity);
        Task<IResult> Delete(T id);
        IDataResult<TEntity> Get(int id);
        List<TEntity> GetAll();
    }
}
