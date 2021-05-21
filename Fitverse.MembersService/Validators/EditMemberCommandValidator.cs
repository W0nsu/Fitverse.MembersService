using System.Linq;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Data;
using FluentValidation;

namespace Fitverse.MembersService.Validators
{
	public class EditMemberCommandValidator : AbstractValidator<EditMemberCommand>
	{
		public EditMemberCommandValidator(MembersContext dbContext)
		{
			RuleFor(x => x.MemberId)
				.GreaterThan(0);

			RuleFor(x => x.MemberId)
				.Must(id => dbContext.Members.Any(m => m.MemberId == id))
				.WithMessage(x => $"Member [MemberId: {x.MemberId}] not found");
		}
	}
}