using System.Collections.Generic;
using Fitverse.MembersService.Dtos;
using MediatR;

namespace Fitverse.MembersService.Queries
{
	public class GetAllAgreementsByMemberIdQuery : IRequest<List<AgreementDto>>
	{
		public GetAllAgreementsByMemberIdQuery(int memberId)
		{
			MemberId = memberId;
		}

		public int MemberId { get; }
	}
}