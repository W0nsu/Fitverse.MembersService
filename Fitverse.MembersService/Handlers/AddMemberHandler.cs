using System;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Helpers;
using Fitverse.MembersService.Interfaces;
using Fitverse.MembersService.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.MembersService.Handlers
{
	public class AddMemberHandler : IRequestHandler<AddMemberCommand, MemberDto>
	{
		private readonly IAddMemberSender _addMemberSender;
		private readonly MembersContext _dbContext;

		public AddMemberHandler(MembersContext dbContext, IAddMemberSender addMemberSender)
		{
			_dbContext = dbContext;
			_addMemberSender = addMemberSender;
		}

		public async Task<MemberDto> Handle(AddMemberCommand request, CancellationToken cancellationToken)
		{
			var pesel = request.Member.Pesel;

			var memberEntity = request.Member.Adapt<Member>();

			memberEntity.BirthDate = BirthDayDateCalculator.ExtractFromPesel(memberEntity.Pesel);

			_ = await _dbContext.AddAsync(memberEntity, cancellationToken);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var newMember = await _dbContext
				.Members
				.SingleOrDefaultAsync(m => m.Pesel == pesel, cancellationToken);

			if (newMember is null)
				throw new NullReferenceException("Failed to add member. Try again");

			var newMemberDto = newMember.Adapt<MemberDto>();

			_addMemberSender.SendMember(newMember);

			return newMemberDto;
		}
	}
}