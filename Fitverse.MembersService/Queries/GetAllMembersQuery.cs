using System.Collections.Generic;
using Fitverse.MembersService.Dtos;
using MediatR;

namespace Fitverse.MembersService.Queries
{
	public class GetAllMembersQuery : IRequest<List<MemberDto>>
	{
	}
}