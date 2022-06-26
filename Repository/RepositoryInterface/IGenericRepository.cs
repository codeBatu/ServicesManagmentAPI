using Model;

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
        IDataResult<TEntity> Get(int id);
        IDataResult<List<TEntity>> GetAll();
    }
}
