using Model;
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

        public Task<LogServiceMessageModel> Create(LogTable entity)
        {
            throw new NotImplementedException();
        }

        public Task<LogServiceMessageModel> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LogTable>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<LogServiceMessageModel> Update(int id, LogTable entity)
        {
            throw new NotImplementedException();
        }
    }
}
