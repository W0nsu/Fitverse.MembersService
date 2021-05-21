using Fitverse.MembersService.Dtos;
using MediatR;

namespace Fitverse.MembersService.Queries
{
	public class GetMemberByIdQuery : IRequest<MemberDto>
	{
		public GetMemberByIdQuery(int id)
		{
			MemberId = id;
		}

		public int MemberId { get; }
	}
}