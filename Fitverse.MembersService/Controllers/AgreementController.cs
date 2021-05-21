using System.Threading.Tasks;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitverse.MembersService.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/ms/agreement")]
	public class AgreementController : Controller
	{
		private readonly IMediator _mediator;

		public AgreementController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("{memberId}")]
		public async Task<IActionResult> GetAllAgreementsByMemberId([FromRoute] int memberId)
		{
			var query = new GetAllAgreementsByMemberIdQuery(memberId);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddAgreement([FromBody] AgreementDto agreement)
		{
			var command = new AddAgreementCommand(agreement);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpGet]
		[Route("memberships")]
		public async Task<IActionResult> GetAllMemberships()
		{
			var query = new GetAllMembershipsQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}
	}
}