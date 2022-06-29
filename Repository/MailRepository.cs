using Model;
using Model.Results;
using Repository.DbContexts;
using Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class MailRepository : IMailRepository
{
    private readonly SmartPulseServiceManagerContext _smartPulseServiceManagerContext;

    public MailRepository(SmartPulseServiceManagerContext smartPulseServiceManagerContext)
    {
        _smartPulseServiceManagerContext = smartPulseServiceManagerContext;
    }

    public async Task<IResult> Create(MailTable entity)
    {
        _smartPulseServiceManagerContext.MailTables.Add(entity);
        var saveResponseCode = await _smartPulseServiceManagerContext.SaveChangesAsync();
        if (saveResponseCode != 1)
        {
            return new ErrorResult("Mail kaydedilemedi!");

        }
        return new SuccessResult("Mail başarıyla kaydedildi.");
    }

    public IDataResult<MailTable> Get(int serviceId)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<MailTable>> GetAll()
    {

        throw new NotImplementedException();
    }
}
