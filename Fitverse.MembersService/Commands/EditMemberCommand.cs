using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Fitverse.MembersService.Commands
{
	public class EditMemberCommand : IRequest<MemberDto>
	{
		public EditMemberCommand(int memberId, JsonPatchDocument<Member> member)
		{
			MemberId = memberId;
			Member = member;
		}

		public int MemberId { get; }
		public JsonPatchDocument<Member> Member { get; }
	}
}