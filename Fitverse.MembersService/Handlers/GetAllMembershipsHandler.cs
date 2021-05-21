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
	public class GetAllMembershipsHandler : IRequestHandler<GetAllMembershipsQuery, List<MembershipDto>>
	{
		private readonly MembersContext _dbContext;

		public GetAllMembershipsHandler(MembersContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<MembershipDto>> Handle(GetAllMembershipsQuery request,
			CancellationToken cancellationToken)
		{
			var membershipsList = await _dbContext
				.Memberships
				.Where(x => !x.IsDeleted)
				.ToListAsync(cancellationToken);

			return membershipsList.Select(membership => membership.Adapt<MembershipDto>()).ToList();
		}
	}
}