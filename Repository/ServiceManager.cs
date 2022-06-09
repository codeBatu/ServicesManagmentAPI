
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
        public async void ActiveService(int id)
        {
            ServiceTable serviceTable = new() { ServiceStatus = (int)ServiceStatusEnum.Active, };
            //   serviceTable.ServiceStatus=(int)ServiceStatusEnum.Active;
            var result = _smartPulseServiceManagerContext!.ServiceTables.FirstOrDefault(t => t.Id == id);
            if (result == null) throw new Exception("Geçersiz Id");
            _smartPulseServiceManagerContext.ServiceTables.Update(serviceTable);
            int saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode != 1)
            {
                throw new Exception("Service active is fail");
            }
        }
        public async void InActiveService(int id)
        {
            ServiceTable serviceTable = new();
            serviceTable.ServiceStatus = (int)ServiceStatusEnum.Inactive;
            var result = _smartPulseServiceManagerContext!.ServiceTables.FirstOrDefault(t => t.Id == id);
            if (result == null) throw new Exception("Geçersiz Id");
            _smartPulseServiceManagerContext.ServiceTables.Update(serviceTable);
            int saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode != 1)
            {
                throw new Exception("Service active is fail");
            }
        }
        public async void RestartService(int id)
        {
            var result = _smartPulseServiceManagerContext?.ServiceTables.SingleOrDefault(t => t.Id == id);
            if (result is null) { throw new Exception("Geçersiz Id"); }
            ServiceTable serviceTable = new();
            serviceTable.RestDateTime = DateTime.Now;
            result.RestartCount += 1;
            _smartPulseServiceManagerContext?.ServiceTables.Update(serviceTable);
            var saveResponseCode = await _smartPulseServiceManagerContext!.SaveChangesAsync();
            if (saveResponseCode != 1)
            {

                throw new Exception("Restart is fail");

            }
        }
        public ServiceTable GetService(int id)
        {

            var result = _smartPulseServiceManagerContext?.ServiceTables.SingleOrDefault(t => t.Id == id);
            TimeSpan timeSpan = new();


            result.ActiveLife = (result.CreateDateTime - DateTime.Now).ToString();
            if (result is null) { return new ServiceTable(); }
            return result;
        }
        public List<ServiceTable> GetAllServices()
        {
            var result = _smartPulseServiceManagerContext?.ServiceTables.ToList();

            if (result is null) { return new List<ServiceTable>(); }
            return result;
        }
        /// <summary>
        /// ServiceName göre servisi tablosundaki veriyi getirir
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ServiceTable GetService(ServiceTable entity)
        {

            var result = _smartPulseServiceManagerContext?.ServiceTables.SingleOrDefault(t => t.ServiceName == entity.ServiceName);
            if (result is null) { throw new Exception("Geçirsiz İsim"); }
            result.ActiveLife = (entity.CreateDateTime - DateTime.Now).ToString();




            return result;

        }
    }
}
