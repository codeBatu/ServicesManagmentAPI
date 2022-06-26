using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("getAllMail")]
        public IActionResult GetAllService()
        {
            var result = mailSupply!.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getMailById")]
        public IActionResult GetLog(int id)
        {
            var result = mailSupply!.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    
}
}
