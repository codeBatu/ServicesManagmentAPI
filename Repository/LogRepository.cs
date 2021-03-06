using Model;
using Model.Results;
using Repository.DbContexts;
using Repository.RepositoryInterface;

namespace Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly SmartPulseServiceManagerDbContext _smartPulseServiceManagerContext;

        public LogRepository(SmartPulseServiceManagerDbContext smartPulseServiceManagerContext)
        {
            _smartPulseServiceManagerContext = smartPulseServiceManagerContext;
        }

        public async Task<IResult> Create(LogTable entity)
        {
            _smartPulseServiceManagerContext!.Add(entity);
            var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
            if (saveResponseCode != 1)
            {
                return new ErrorResult("Log kaydedilemedi!");

            }
            return new SuccessResult("Log başarıyla kaydedildi.");
        }

        public IDataResult<LogTable> Get(int logId)
        {
            var log = _smartPulseServiceManagerContext?.LogTable.SingleOrDefault(l => l.Id == logId);
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
