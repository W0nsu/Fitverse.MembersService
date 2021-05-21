using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Queries;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.MembersService.Handlers
{
	public class GetAllMembersHandler : IRequestHandler<GetAllMembersQuery, List<MemberDto>>
	{
		private readonly MembersContext _dbContext;

		public GetAllMembersHandler(MembersContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<MemberDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
		{
			var membersList = await _dbContext.Members.ToListAsync(cancellationToken);

			return membersList.Select(member => member.Adapt<MemberDto>()).ToList();
		}
	}
}