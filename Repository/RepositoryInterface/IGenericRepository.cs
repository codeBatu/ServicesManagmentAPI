using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IGenericRepository<TEntity, TServiceMessageModel, T>
    {
        Task<TServiceMessageModel> Create(TEntity entity);
        Task<TServiceMessageModel> Update(T id, TEntity entity);
        Task<TServiceMessageModel> Delete(T id);
        Task<List<TEntity>> GetAll();
    }
}
