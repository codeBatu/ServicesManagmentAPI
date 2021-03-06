
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Results;
using Repository.DbContexts;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ServiceManagerRepository : IServiceManagerRepository
    {
        private readonly SmartPulseServiceManagerDbContext? _smartPulseServiceManagerContext;

        public ServiceManagerRepository(SmartPulseServiceManagerDbContext? smartPulseServiceManagerContext)
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
        public async Task<IResult> Create(ServiceTable entity)
        {
            // isme göre servis bilgisi getir
            var result = GetService(entity.ServiceName);
            // eğer aynı isimde bir servis varsa error result dön
            if (result.Success)
            {
                return new ErrorResult("Aynı isimde servis sistemde kayıtlı.");
            }

            _smartPulseServiceManagerContext!.Add(entity);
            var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorResult("Servis kaydedilemedi!");

            }
            return new SuccessResult("Servis başarıyla kaydedildi.");
        }
        public async Task<IResult> Update(int id, ServiceTable entity)
        {
            var service = _smartPulseServiceManagerContext!.ServiceTable.FirstOrDefault(t => t.Id == id);
            if (service is null)
            {
                return new ErrorResult("Geçersiz id");
            }

            // güncellenen isimde başka bir servis sistemde kayıtlı mı diye kontrol et
            var result = GetService(entity.ServiceName);
            if (result.Success && service.Id != result.Data.Id)
            {
                return new ErrorResult("Aynı isimde servis sistemde kayıtlı.");
            }

            service.ServiceName = entity.ServiceName;
            service.Version = entity.Version;

            _smartPulseServiceManagerContext.Update(service);
            var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorResult("Servis güncellenemedi!");

            }
            return new SuccessResult("Servis başarıyla güncellendi.");
        }

        /// <summary>
        /// Id göre servisi tablosundaki veriyi siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> Delete(int id)
        {
            var result = _smartPulseServiceManagerContext!.ServiceTable.FirstOrDefault(t => t.Id == id);
            if (result is null)
            {
                return new ErrorResult("Geçersiz id");
            }
            result.IsDelete = true;
            _smartPulseServiceManagerContext.ServiceTable.Update(result);
            //    _smartPulseServiceManagerContext!.ServiceTable.Remove(result);
            var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorResult("Servis silinemedi!");
            }
            return new SuccessResult("Servis başarıyla silindi.");
        }

        // Servisi aktive eder.
        public async Task<IResult> ActiveService(int id)
        {
            var result = _smartPulseServiceManagerContext!.ServiceTable.FirstOrDefault(t => t.Id == id);
            if (result == null) return new ErrorResult("Geçersiz id");

            if (result.ServiceStatus == ServiceStatusEnum.Active)
                return new ErrorResult("Servis zaten aktif!");

            result.ServiceStatus = ServiceStatusEnum.Active;
            _smartPulseServiceManagerContext.ServiceTable.Update(result);
            int saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorResult("Servis aktive edilemedi!");
            }
            return new SuccessResult("Servis başarıyla aktive edildi.");
        }
        public async Task<IResult> InActiveService(int id)
        {
            var result = _smartPulseServiceManagerContext!.ServiceTable.FirstOrDefault(t => t.Id == id);
            if (result == null) return new ErrorResult("Geçersiz id");

            if (result.ServiceStatus == ServiceStatusEnum.Inactive)
                return new ErrorResult("Servis zaten inaktif!");

            result.ServiceStatus = ServiceStatusEnum.Inactive;
            _smartPulseServiceManagerContext.ServiceTable.Update(result);
            int saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorResult("Servis deaktive edilemedi!");
            }
            return new SuccessResult("Servis başarıyla deaktive edildi.");
        }
        public async Task<IResult> RestartService(int id)
        {
            var result = _smartPulseServiceManagerContext?.ServiceTable.SingleOrDefault(t => t.Id == id);
            if (result is null) return new ErrorResult("Geçersiz id");

            result.RestDateTime = DateTime.Now;
            result.RestartCount += 1;
            _smartPulseServiceManagerContext?.ServiceTable.Update(result);
            var saveResponseCode = await _smartPulseServiceManagerContext!.SaveChangesAsync();
            if (saveResponseCode < 1)
            {
                return new ErrorResult("Servis restart edilemedi!");
            }
            return new SuccessResult("Servis başarıyla restart edildi.");
        }

        /// <summary>
        /// ServiceName göre servisi tablosundaki veriyi getirir
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IDataResult<ServiceTable> GetService(string name)
        {
            var result = _smartPulseServiceManagerContext?.ServiceTable.Include(s => s.LogTables).Where(s=>s.IsDelete==false).SingleOrDefault(t => t.ServiceName == name);
            if (result is null) return new ErrorDataResult<ServiceTable>("Bu isimde bir servis bulunamadı!");
            result.ActiveLife = (DateTime.Now - result.CreateDateTime).ToString();
            return new SuccessDataResult<ServiceTable>(result);
        }

        public IDataResult<ServiceTable> Get(int id)
        {
            var service = _smartPulseServiceManagerContext?.ServiceTable.Include(s => s.LogTables).Where(s => s.IsDelete == false).SingleOrDefault(s => s.Id == id);
            if (service is null) return new ErrorDataResult<ServiceTable>("Geçersiz id!");
            service.ActiveLife = (DateTime.Now - service.CreateDateTime).ToString();
            return new SuccessDataResult<ServiceTable>(service);
        }

        public IDataResult<List<ServiceTable>> GetAll()
        {
            var list = _smartPulseServiceManagerContext!.ServiceTable.Include(s => s.LogTables).Where(s => s.IsDelete == false).ToList();
            foreach (var service in list)
            {
                service.ActiveLife = (DateTime.Now - service.CreateDateTime).ToString();
            }
            return new SuccessDataResult<List<ServiceTable>>(list);
        }
    }
}
