using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryInterface;

public interface IMailRepository : IGenericRepository<MailTable, int>
{
}
