using Model;
using Repository;
using Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ServicesLayer
    {
    

        private readonly IServiceManager<ServiceTable, ServiceMassageModel, int> _serviceManager;
        private readonly SmartPulseServiceManagerContext? _smartPulseServiceManagerContext;
        public ServicesLayer(IServiceManager<ServiceTable, ServiceMassageModel, int> serviceManager, SmartPulseServiceManagerContext? smartPulseServiceManagerContext)
        {
            _smartPulseServiceManagerContext = smartPulseServiceManagerContext;
            _serviceManager = serviceManager;
        }
        LogTable logTable = new();
        ServiceMassageModel serviceMassageModel = new();
        private Guid guidGenerator = Guid.NewGuid();
        public async void ActiveService(int id)
        {

            try
            {

                _serviceManager.ActiveService(id);
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = "Service is active";


            }
            catch (Exception e)
            {
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = e.ToString();

            }
            _smartPulseServiceManagerContext!.LogTables.Add(logTable);
            await _smartPulseServiceManagerContext.SaveChangesAsync();
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

            var result = await _serviceManager.CreateService(entity);
            if (result.Status == false)
            {
                logTable.ServiceId = entity.Id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = result.Message;
            }
            else
            {
                logTable.ServiceId = entity.Id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = result.Message;

            }
            _smartPulseServiceManagerContext!.LogTables.Add(logTable);
            await _smartPulseServiceManagerContext.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Id göre servisi tablosundaki veriyi siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceMassageModel> DeleteService(int id)
        {
            var result = await _serviceManager.DeleteService(id);
            if (result.Status == false)
            {
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = result.Message;
            }
            else
            {
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = result.Message;

            }
            _smartPulseServiceManagerContext!.LogTables.Add(logTable);
            await _smartPulseServiceManagerContext.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Id göre servisi tablosundaki veriyi getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceTable GetService(int id)
        {
            return  _serviceManager.GetService(id);
         
     
        }
        /// <summary>
        /// ServiceName göre servisi tablosundaki veriyi getirir
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ServiceTable GetService(ServiceTable entity)
        {
            return _serviceManager.GetService(entity);

        }
        public List<ServiceTable> GetAllService()
        {
            return _serviceManager.GetAllServices();

        }

        public async void InActiveService(int id)
        {
            try
            {

                _serviceManager.InActiveService(id);
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = "Service is active";


            }
            catch (Exception e)
            {
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = e.ToString();

            }
            _smartPulseServiceManagerContext!.LogTables.Add(logTable);
            await _smartPulseServiceManagerContext.SaveChangesAsync();
        }

        public async void RestartService(int id)
        {
            try
            {

                _serviceManager.RestartService(id);
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = "Restart Service";


            }
            catch (Exception e)
            {
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = e.ToString();

            }
            _smartPulseServiceManagerContext!.LogTables.Add(logTable);
            await _smartPulseServiceManagerContext.SaveChangesAsync();
        }

        public async Task<ServiceMassageModel> UpdateService(int id, ServiceTable entity)
        {
            var result = await _serviceManager.UpdateService(id,entity);
            if (result.Status == false)
            {
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = result.Message;
            }
            else
            {
                logTable.ServiceId = id;
                logTable.TraceId = guidGenerator.ToString();
                logTable.Contents = result.Message;

            }
            _smartPulseServiceManagerContext!.LogTables.Add(logTable);
            await _smartPulseServiceManagerContext.SaveChangesAsync();
            return result;
        }
    }
}
