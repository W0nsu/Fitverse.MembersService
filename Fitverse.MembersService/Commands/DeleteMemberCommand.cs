using Fitverse.MembersService.Dtos;
using MediatR;

namespace Fitverse.MembersService.Commands
{
	public class DeleteMemberCommand : IRequest<MemberDto>
	{
		public DeleteMemberCommand(int memberId)
		{
			MemberId = memberId;
		}

		public int MemberId { get; }
	}
}