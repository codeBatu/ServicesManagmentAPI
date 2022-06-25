using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IServiceManagerRepository : IGenericRepository<ServiceTable, ServiceMassageModel, int>
    {

        void RestartService(int id);
        void ActiveService(int id);
        void InActiveService(int id);

        ServiceTable GetServiceById(int id);
        ServiceTable GetService(ServiceTable entity);
      

    }
}

