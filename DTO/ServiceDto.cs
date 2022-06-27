using AutoMapper;
using Business.Abstract;
using Model;
using Model.EntityDto;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ServiceDto
    {
        private readonly IServiceSupply? _serviceManager;
        private readonly IMapper _mapper;
        public ServiceDto(IServiceSupply? serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }
        public async Task<IResult> ActiveService(int id)
        {
          return await _serviceManager!.ActiveService(id);
          
        }

        public async Task<IResult> Create(ServiceTableDtoEntity entity)
        {
       
     var result= await _serviceManager!.Create(_mapper.Map<ServiceTable>(entity));
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

           
            return new SuccessResult(result.Message);
        }

        public async Task<IResult> Delete(int id)
        {var result = await _serviceManager!.Delete(id);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }


            return new SuccessResult(result.Message);
          

        }

        public IDataResult<ServiceTable> Get(int id)
        {
            return _serviceManager.Get(id);
        }

        public IDataResult<List<ServiceTable>> GetAll()
        {
            return _serviceManager.GetAll();
        }

        public IDataResult<ServiceTable> GetService(ServiceTableDtoEntity entity)
        {
          
            return _serviceManager.GetService(_mapper.Map<ServiceTable>(entity));
        }

        public async Task<IResult> InActiveService(int id)
        {
            var result = await _serviceManager!.InActiveService(id);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

         

            return new SuccessResult(result.Message);
        }

        public async Task<IResult> RestartService(int id)
        {
            var result = await _serviceManager.RestartService(id);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

          
            return new SuccessResult(result.Message);
        }

        public async Task<IResult> Update(int id, ServiceTableDtoEntity entity)
        {
            var result = await _serviceManager!.Update(id, _mapper.Map<ServiceTable>(entity));
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }


            return new SuccessResult(result.Message);
        }
    }
}
