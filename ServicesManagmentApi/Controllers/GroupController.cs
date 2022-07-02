using Business.Abstract;
using Business.Helpers.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTOs.Accounts;
using Model.DTOs.Group;
using Model.Results;
using Repository.RepositoryInterface;

namespace ServicesManagmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupSupply groupRepository;

        public GroupController(IGroupSupply groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        [Authorize(Role.Admin)]
        [HttpPost("createGroup")]
        public async Task<ActionResult<UserGroup>> Create([FromBody] GroupDto model)
        {
            var grp = new UserGroup { GroupName = model.Name };
            var result = await groupRepository.Create(grp);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [Authorize(Role.Admin)]
        [HttpPost("AddGroupAdmin")]
        public async Task<ActionResult<UserGroup>> AddGroupAdmin([FromBody] AccountMemberDto model,int id)
        {
            var grp = new Account { Id = model.Id };
            var result = await groupRepository.AddGroupAdmin(grp,id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [Authorize(Role.Admin, Role.GroupAdmin)]
        [HttpPost("AddGroupUser")]
        public async Task<ActionResult<UserGroup>> AddGroupMember([FromBody]  AccountMemberDto memberDto, int id)
        {
            var grp = new Account { Id = memberDto.Id };
            var result = await groupRepository.AddUserGroup(grp.Id, id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [Authorize(Role.Admin)]
        [HttpGet("getAllGroup")]
        public IActionResult GetAllService()
        {
            var result = groupRepository!.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [Authorize(Role.Admin)]
        [HttpGet("GetServiceById")]
        public IActionResult GetById(int id)
        {
            var result = groupRepository!.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
