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
    public class GroupController : BaseController
    {
        private readonly IGroupSupply groupRepository;

        public GroupController(IGroupSupply groupRepository)
        {
            this.groupRepository = groupRepository;
        }
    
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
     
        [HttpGet("getAllGroup")]
        public IActionResult GetAllService()
        {

            if (Account.Role == Role.GroupAdmin)
            {

                var userGroup = groupRepository.Get((int)Account.UserGroupId);
               
                var list = new List<UserGroup> { userGroup.Data };

                return Ok(list);
                

            }
            var result = groupRepository!.GetAll();
            if (Account.Role == Role.User)
            {
                return Unauthorized();
            }
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
      
        [HttpGet("GetGroupById")]
        public IActionResult GetById(int id)
        {
            var result = groupRepository!.Get(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("updateGroup")]
        public async Task<IActionResult> UpdateGroup(int id, GroupDto groupDto)
        {
            var group = new UserGroup { GroupName = groupDto.Name };
            var result = await groupRepository!.Update(id, group);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
