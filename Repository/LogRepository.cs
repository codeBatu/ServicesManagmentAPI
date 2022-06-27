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
    public class LogRepository : ILogRepository
    {
        private readonly SmartPulseServiceManagerContext _smartPulseServiceManagerContext;

        public LogRepository(SmartPulseServiceManagerContext smartPulseServiceManagerContext)
        {
            _smartPulseServiceManagerContext = smartPulseServiceManagerContext;
        }

        public async Task<IResult> Create(LogTable entity)
        {
            entity.CreateDateTime = DateTime.Now;
            _smartPulseServiceManagerContext!.Add(entity);
            var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode != 1)
            {
                return new ErrorResult("Log kaydedilemedi!");

            }
            return new SuccessResult("Log başarıyla kaydedildi.");
        }

        public IDataResult<LogTable> Get(int serviceId)
        {
            var log = _smartPulseServiceManagerContext?.LogTable.SingleOrDefault(l => l.ServiceId == serviceId);
            if (log is null) return new ErrorDataResult<LogTable>("Geçersiz id!");
            return new SuccessDataResult<LogTable>(log);
        }

        public IDataResult<List<LogTable>> GetAll()
        {
            var list = _smartPulseServiceManagerContext.LogTable.ToList();
            return new SuccessDataResult<List<LogTable>>(list);
        }
    }
}
