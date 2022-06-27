using Business.Abstract;

using Model.Results;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LogManager  :ILogSupply
    {
        ILogRepository _logDal;

        public LogManager(ILogRepository logDal)
        {
            _logDal = logDal;
        }

        public IDataResult<Model.LogTable> Get(int id)
        {
           return _logDal.Get(id);
        }

        public IDataResult<List<Model.LogTable>> GetAll()
        {
           
           return _logDal.GetAll();
        }
    }
}
