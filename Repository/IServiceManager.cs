using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IServiceManager<TEntity, TServiceMessageModel, T> where TEntity : class
    {

        Task<TServiceMessageModel> CreateService(TEntity entity);
        Task<TServiceMessageModel> UpdateService(T id, TEntity entity);
        Task<TServiceMessageModel> DeleteService(T id);
        void RestartService(T id);
        void ActiveService(T id);
        void InActiveService(T id);

        TEntity GetService(int id);
        TEntity GetService(TEntity entity);
        List<TEntity> GetAllServices();

    }
}

