﻿using Model;
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

        public Task<IResult> Create(LogTable entity)
        {
            throw new NotImplementedException();
        }


        public IDataResult<LogTable> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<LogTable>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
