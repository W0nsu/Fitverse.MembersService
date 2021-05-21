using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Interfaces;
using Fitverse.MembersService.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.MembersService.Handlers
{
	public class AddAgreementHandler : IRequestHandler<AddAgreementCommand, AgreementDto>
	{
		private readonly IAddAgreementSender _addAgreementSender;
		private readonly MembersContext _dbContext;

		public AddAgreementHandler(MembersContext dbContext, IAddAgreementSender addAgreementSender)
		{
			_dbContext = dbContext;
			_addAgreementSender = addAgreementSender;
		}

		public async Task<AgreementDto> Handle(AddAgreementCommand request, CancellationToken cancellationToken)
		{
			var agreementEntity = request.Agreement.Adapt<Agreement>();

			var membershipEntity = await _dbContext
				.Memberships
				.FirstOrDefaultAsync(m => m.Id == agreementEntity.MembershipId,
					cancellationToken);

			if (membershipEntity is not null)
				agreementEntity.Name = membershipEntity.Name;

			_ = await _dbContext.AddAsync(agreementEntity, cancellationToken);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var newAgreement = _dbContext
				.Agreements
				.Where(m => m.MemberId == request.Agreement.MemberId)
				.AsEnumerable()
				.LastOrDefault();

			if (newAgreement is null)
				throw new NullReferenceException("Failed to add agreement. Try again");

			var newAgreementDto = newAgreement.Adapt<AgreementDto>();

			_addAgreementSender.SendAgreement(agreementEntity);

			return newAgreementDto;
		}
	}
}