
using Model;
using Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ServiceManager : IServiceManager<ServiceTable,ServiceMassageModel,int>
    {
        private readonly SmartPulseServiceManagerContext? _smartPulseServiceManagerContext;

        public ServiceManager(SmartPulseServiceManagerContext? smartPulseServiceManagerContext)
        {
            _smartPulseServiceManagerContext = smartPulseServiceManagerContext;
        }

      

        /// <summary>
        /// Service tablosuna veri ekler
        /// Service Name aynı isimde veri varsa ekleme yapmaz
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>ServiceMassageModel Döner</returns>
        public async Task<ServiceMassageModel> CreateService(ServiceTable entity)
        {
            ServiceMassageModel serviceMassageModel = new();
            serviceMassageModel.Status = true;
            serviceMassageModel.Message = "Create is Succes";
      var result=      _smartPulseServiceManagerContext!.ServiceTables.FirstOrDefault(t=>t.ServiceName==entity.ServiceName);
            if(result is not null)
            {
                serviceMassageModel.Status = false;
                serviceMassageModel.Message = "ServiceName is registered ";
                throw new Exception(serviceMassageModel.Message);
            }
            _smartPulseServiceManagerContext!.Add(entity);
        var saveResponseCode =    await _smartPulseServiceManagerContext.SaveChangesAsync();
            if(saveResponseCode != 1)
            {
                serviceMassageModel.Status = false;
                serviceMassageModel.Message = "Create is Fail ";
                throw new Exception(serviceMassageModel.Message);

            }
            return serviceMassageModel;
      
       
        }
        public async Task<ServiceMassageModel> UpdateService(int id, ServiceTable entity)
        {
            ServiceMassageModel serviceMassageModel = new();
            serviceMassageModel.Status = true;
            serviceMassageModel.Message = "Update is Succes";
            var result = _smartPulseServiceManagerContext!.ServiceTables.FirstOrDefault(t => t.Id == id);
            if (result is null)
            {
                serviceMassageModel.Status = false;
                serviceMassageModel.Message = "Geçersiz Id ";
                throw new Exception(serviceMassageModel.Message);

            }
            _smartPulseServiceManagerContext.Update(entity);
            var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode != 1)
            {
                serviceMassageModel.Status = false;
                serviceMassageModel.Message = "Update is Fail ";
                throw new Exception(serviceMassageModel.Message);

            }
            return serviceMassageModel;
        }   /// <summary>
            /// Id göre servisi tablosundaki veriyi siler
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        public async Task<ServiceMassageModel> DeleteService(int id)
        {
            ServiceMassageModel serviceMassageModel = new();
            serviceMassageModel.Status = true;
            serviceMassageModel.Message = "Delete is Succes";
            var result = _smartPulseServiceManagerContext!.ServiceTables.FirstOrDefault(t => t.Id == id);
            if (result is null)
            {
                serviceMassageModel.Status = false;
                serviceMassageModel.Message = "Geçersiz Id ";
                throw new Exception(serviceMassageModel.Message);

            }
            var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode != 1)
            {
                serviceMassageModel.Status = false;
                serviceMassageModel.Message = "Remove is Fail ";
                throw new Exception(serviceMassageModel.Message);

            }
            return serviceMassageModel;
        }
    }
}
