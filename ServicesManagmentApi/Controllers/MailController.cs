using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ServicesManagmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailSupply? mailSupply;

        public MailController(IMailSupply? mailSupply)
        {
            this.mailSupply = mailSupply;
        }

        [HttpPost("sendMail")]
        public IActionResult GetAllService([FromBody] MailTable mailTable)
        {
            var result = mailSupply!.Create(mailTable);
            if (!result.IsCompletedSuccessfully)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
      

    
}
}
