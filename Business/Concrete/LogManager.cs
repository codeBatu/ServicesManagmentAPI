using Business.Abstract;
using Business.Helpers;
using Model;
using Model.Results;
using Repository.RepositoryInterface;

namespace Business.Concrete;

public class LogManager : ILogSupply
{
    ILogRepository _logDal;
    IEmailService _mailService;
    IMailSupply _mailDal;

    public LogManager(ILogRepository logDal, IEmailService mailService,IMailSupply mailDal)
    {
        _logDal = logDal;
        _mailDal = mailDal;
        _mailService = mailService;
    }

    public IDataResult<LogTable> Get(int id)
    {
        return _logDal.Get(id);
    }

    public IDataResult<List<LogTable>> GetAll()
    {

        return _logDal.GetAll();
    }

    public async Task<IResult> Create(LogTable entity)
    {
        entity.CreateDateTime = DateTime.Now;
        var result = await _logDal.Create(entity);

        MailTable mail = new() { Body = entity.Contents, LogId = entity.Id, Sender = "info@service-management-webapi.io", Gmail = "syrach3tudd2tjha@ethereal.email", Topic = "Log Oluşturuldu" };
        _mailService.Send(mail.Gmail, mail.Topic, mail.Body);
        await _mailDal.Create(mail);

        return result;
    }
}
