using System;
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
	public class GetAllAgreementsByMemberIdHandler
		: IRequestHandler<GetAllAgreementsByMemberIdQuery, List<AgreementDto>>
	{
		private readonly MembersContext _dbContext;

		public GetAllAgreementsByMemberIdHandler(MembersContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<AgreementDto>> Handle(GetAllAgreementsByMemberIdQuery request,
			CancellationToken cancellationToken)
		{
			var agreementsList = await _dbContext
				.Agreements.Where(m => m.MemberId == request.MemberId)
				.OrderByDescending(a => a.StartingDate)
				.ToListAsync(cancellationToken);

			if (agreementsList is null)
				throw new NullReferenceException($"Agreements for member [MembershipId: {request.MemberId}] not found");

			return agreementsList.Select(agreement => agreement.Adapt<AgreementDto>()).ToList();
		}
	}
}