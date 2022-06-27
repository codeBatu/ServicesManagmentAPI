﻿
using Model;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract;

public interface IServiceSupply
{
    IDataResult<ServiceTable> Get(int id);
    IDataResult<List<ServiceTable>> GetAll();

    Task<IResult> Create(ServiceTable entity);
    Task<IResult> Update(int id, ServiceTable entity);
    Task<IResult> Delete(int id);

    Task<IResult> RestartService(int id);
    Task<IResult> ActiveService(int id);
    Task<IResult> InActiveService(int id);

    IDataResult<ServiceTable> GetService(string name);
}
