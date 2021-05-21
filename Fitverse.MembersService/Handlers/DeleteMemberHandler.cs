using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.MembersService.Handlers
{
	public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand, MemberDto>
	{
		private readonly MembersContext _dbContext;
		private readonly IDeleteMemberSender _deleteMemberSender;

		public DeleteMemberHandler(MembersContext dbContext, IDeleteMemberSender deleteMemberSender)
		{
			_dbContext = dbContext;
			_deleteMemberSender = deleteMemberSender;
		}

		public async Task<MemberDto> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
		{
			var memberEntity = await _dbContext
				.Members
				.SingleOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

			if (memberEntity is null)
				throw new NullReferenceException($"Member [memberId: {request.MemberId}] not found");

			_dbContext.Remove(memberEntity);
			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var deletedMemberAgreements = await _dbContext
				.Agreements
				.Where(x => x.MemberId == request.MemberId)
				.ToListAsync(cancellationToken);

			foreach (var agreement in deletedMemberAgreements)
				_ = _dbContext.Remove(agreement);

			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			_deleteMemberSender.DeleteMember(memberEntity.MemberId);

			var memberDto = memberEntity.Adapt<MemberDto>();

			return memberDto;
		}
	}
}