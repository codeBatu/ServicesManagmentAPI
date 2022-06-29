using Business.Abstract;
using Model;
using Model.Results;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class MailManager : IMailSupply
{
    IMailRepository mailRepository;

    public MailManager(IMailRepository mailRepository)
    {
        this.mailRepository = mailRepository;
    }

    public async Task<IResult> Create(MailTable entity)
    {
        return await mailRepository.Create(entity);
        
    }


}
