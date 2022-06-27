using Model;
using Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Abstract
{
    public interface IMailSupply
    {
   
     Task<IResult> Create(MailTable entity);
    }

}
