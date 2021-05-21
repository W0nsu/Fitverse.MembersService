using System.Threading.Tasks;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Models;
using Fitverse.MembersService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Fitverse.MembersService.Controllers
{
	[Authorize]
	[Route("api/ms/members")]
	[ApiController]
	public class MemberController : ControllerBase
	{
		private readonly IMediator _mediator;

		public MemberController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllMembers()
		{
			var query = new GetAllMembersQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetMemberById([FromRoute] int id)
		{
			var query = new GetMemberByIdQuery(id);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddMember([FromBody] MemberDto memberDto)
		{
			var command = new AddMemberCommand(memberDto);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> EditMember([FromRoute] int id,
			[FromBody] JsonPatchDocument<Member> memberEntity)
		{
			var command = new EditMemberCommand(id, memberEntity);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMember([FromRoute] int id)
		{
			var command = new DeleteMemberCommand(id);
			var result = await _mediator.Send(command);
			return Ok($"Member [memberId: {result.MemberId}] has been deleted");
		}
	}
}