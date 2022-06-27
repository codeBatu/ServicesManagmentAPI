using Business.Abstract;
using Model;
using Model.Results;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class ServiceManager : IServiceSupply
{
    IServiceManagerRepository _serviceDal;
    ILogRepository _logDal;

    public ServiceManager(IServiceManagerRepository serviceDal, ILogRepository logDal)
    {
        _serviceDal = serviceDal;
        _logDal = logDal;
    }

    private async Task AddLog(int serviceId, string content)
    {
        Guid guid = Guid.NewGuid();
        LogTable log = new() { ServiceId = serviceId, Contents = content, CreateDateTime = DateTime.Now, TraceId = guid.ToString() };
        await _logDal.Create(log);
    }

    public async Task<IResult> ActiveService(int id)
    {
        var result = await _serviceDal.ActiveService(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(id, "Servis aktive edildi.");
        return new SuccessResult(result.Message);
    }

    public async Task<IResult> Create(ServiceTable entity)
    {
        entity.CreateDateTime = DateTime.Now;
        entity.RestartCount = 0;

        var result = await _serviceDal.Create(entity);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(entity.Id, "Servis sisteme eklendi.");
        return new SuccessResult(result.Message);
    }

    public async Task<IResult> Delete(int id)
    {
        var result = await _serviceDal.Delete(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult(result.Message);
    }

    public IDataResult<ServiceTable> Get(int id)
    {
        return _serviceDal.Get(id);
    }

    public IDataResult<List<ServiceTable>> GetAll()
    {
        return _serviceDal.GetAll();
    }

    public IDataResult<ServiceTable> GetService(string name)
    {
        return _serviceDal.GetService(name);
    }

    public async Task<IResult> InActiveService(int id)
    {
        var result = await _serviceDal.InActiveService(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(id, "Servis deaktive edildi.");
        return new SuccessResult(result.Message);
    }

    public async Task<IResult> RestartService(int id)
    {
        var result = await _serviceDal.RestartService(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(id, "Servis restart edildi.");
        return new SuccessResult(result.Message);
    }

    public async Task<IResult> Update(int id, ServiceTable entity)
    {
        var result = await _serviceDal.Update(id, entity);
        if(!result.Success)
        {
            return new ErrorResult(result.Message);
        }
        await AddLog(id, "Servis bilgileri güncellendi.");
        return new SuccessResult(result.Message);
    }
}
