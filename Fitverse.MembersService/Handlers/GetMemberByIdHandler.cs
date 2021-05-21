using System;
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
	public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, MemberDto>
	{
		private readonly MembersContext _dbContext;


		public GetMemberByIdHandler(MembersContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<MemberDto> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
		{
			var memberEntity = await _dbContext
				.Members
				.SingleOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

			if (memberEntity is null)
				throw new NullReferenceException($"Member [memberId: {request.MemberId}] not found");

			var memberDto = memberEntity.Adapt<MemberDto>();

			return memberDto;
		}
	}
}