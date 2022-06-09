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
     
            


    }
}

