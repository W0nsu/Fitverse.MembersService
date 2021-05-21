using System;
using System.Linq;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Data;
using FluentValidation;

namespace Fitverse.MembersService.Validators
{
	public class AddAgreementCommandValidator : AbstractValidator<AddAgreementCommand>
	{
		public AddAgreementCommandValidator(MembersContext dbContext)
		{
			RuleFor(x => x.Agreement.MembershipId)
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(x => x.Agreement.MembershipId)
				.Must(id => dbContext.Memberships.Any(m => m.MembershipId == id))
				.WithMessage(x => $"Membership [MembershipId: {x.Agreement.MembershipId}] not found.");

			RuleFor(x => x.Agreement.MemberId)
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(x => x.Agreement.MemberId)
				.Must(id => dbContext.Members.Any(m => m.MemberId == id))
				.WithMessage(x => $"Member [MemberId: {x.Agreement.MemberId}] not found.");

			RuleFor(x => x.Agreement.StartingDate)
				.NotEmpty();

			RuleFor(x => x.Agreement.StartingDate)
				.Must(startingDate => startingDate >= DateTime.Now.Date)
				.WithMessage($"Select a date later than {DateTime.Now.Date}");
			;
		}
	}
}