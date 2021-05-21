using Fitverse.MembersService.Dtos;
using MediatR;

namespace Fitverse.MembersService.Commands
{
	public class AddMemberCommand : IRequest<MemberDto>
	{
		public AddMemberCommand(MemberDto memberDto)
		{
			Member = memberDto;
		}

		public MemberDto Member { get; }
	}
}