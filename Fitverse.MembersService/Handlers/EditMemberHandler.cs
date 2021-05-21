using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Dtos;
using Fitverse.MembersService.Helpers;
using Fitverse.MembersService.Interfaces;
using Fitverse.MembersService.Models;
using Fitverse.MembersService.Validators;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.MembersService.Handlers
{
	public class EditMemberHandler : ControllerBase, IRequestHandler<EditMemberCommand, MemberDto>
	{
		private readonly MembersContext _dbContext;
		private readonly IEditMemberSender _editMemberSender;

		public EditMemberHandler(MembersContext dbContext, IEditMemberSender editMemberSender)
		{
			_dbContext = dbContext;
			_editMemberSender = editMemberSender;
		}

		public async Task<MemberDto> Handle(EditMemberCommand request, CancellationToken cancellationToken)
		{
			var memberEntity = await _dbContext
				.Members
				.SingleOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

			if (memberEntity is null)
				throw new NullReferenceException($"Member [memberId: {request.MemberId}] not found");

			var memberBeforeChange = memberEntity;

			var editedMember = request.Member;
			editedMember.ApplyTo(memberEntity, ModelState);

			memberEntity.BirthDate = BirthDayDateCalculator.ExtractFromPesel(memberEntity.Pesel);

			var memberAfterChange = memberEntity;
			var validator =
				new MemberValidator(_dbContext, new Tuple<Member, Member>(memberBeforeChange, memberAfterChange));
			var validationResult = await validator.ValidateAsync(memberEntity, cancellationToken);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult.Errors.ToList());

			_ = await _dbContext.SaveChangesAsync(cancellationToken);

			var patchedMemberEntity = await _dbContext
				.Members
				.SingleOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

			if (patchedMemberEntity is null)
				throw new NullReferenceException($"Failed to fetch patched membership [Id: {request.MemberId}]");

			_editMemberSender.EditMember(patchedMemberEntity);

			var patchedMemberDto = patchedMemberEntity.Adapt<MemberDto>();

			return patchedMemberDto;
		}
	}
}