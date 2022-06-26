using Business.Abstract;
using Model;
using Model.Results;
using Repository;
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

    public ServiceManager(IServiceManagerRepository serviceDal, ILogRepository logDal, IMailRepository mailRepository)
    {
        _serviceDal = serviceDal;
        _logDal = logDal;
      
    }

    private async Task AddLog(int serviceId, string content,string servisStatus)
    {
        Guid guid = Guid.NewGuid();
       
        LogTable log = new LogTable { ServiceId = serviceId, Contents = $"Servis {servisStatus} edildi.", CreateDateTime = DateTime.Now, TraceId = guid.ToString() };
        await _logDal.Create(log);
    }
 

    public async Task<IResult> ActiveService(int id)
    {
        var result = await _serviceDal.ActiveService(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

      await AddLog(id, "Servis  edildi.","active");
      
        
        return new SuccessResult(result.Message);
    }

    public async Task<IResult> Create(ServiceTable entity)
    {
        var result = await _serviceDal.Create(entity);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(entity.Id, "Servis sisteme eklendi.","");
     
        return new SuccessResult(result.Message);
    }

    public async Task<IResult> Delete(int id)
    {
        var result = await _serviceDal.Delete(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(id, "Servis sistemden silindi.","");
    
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

    public IDataResult<ServiceTable> GetService(ServiceTable entity)
    {
        return _serviceDal.GetService(entity);
    }

    public async Task<IResult> InActiveService(int id)
    {
        var result = await _serviceDal.InActiveService(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(id, "Servis  edildi.","InActive");
     

        return new SuccessResult(result.Message);
    }

    public async Task<IResult> RestartService(int id)
    {
        var result = await _serviceDal.RestartService(id);
        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        await AddLog(id, "Servis  edildi.", "restart");
      
        return new SuccessResult(result.Message);
    }

    public Task<IResult> Update(int id, ServiceTable entity)
    {
        throw new NotImplementedException();
    }
}
