﻿using Model;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface
{
    public interface IServiceManagerRepository : IGenericRepository<ServiceTable, int>
    {

        Task<IResult> RestartService(int id);
        Task<IResult> ActiveService(int id);
        Task<IResult> InActiveService(int id);

        IDataResult<ServiceTable> GetService(ServiceTable entity);
      

    }
}

