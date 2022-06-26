using Business.Abstract;
using Model;
using Model.Results;
using Repository;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MailManager : IMailSupply
    {
        IMailRepository mailRepository;

        public MailManager(IMailRepository mailRepository)
        {
            this.mailRepository = mailRepository;
        }

        public IDataResult<MailTable> Get(int id)
        {
           return mailRepository.Get(id);
        }

        public IDataResult<List<MailTable>> GetAll()
        {
            return mailRepository.GetAll();
        }
    }
}
