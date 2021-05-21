using System.Linq;
using Fitverse.MembersService.Commands;
using Fitverse.MembersService.Data;
using FluentValidation;

namespace Fitverse.MembersService.Validators
{
	public class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
	{
		public AddMemberCommandValidator(MembersContext dbContext)
		{
			RuleFor(x => x.Member.Name)
				.NotEmpty();

			RuleFor(x => x.Member.SurName)
				.NotEmpty();


			RuleFor(x => x.Member.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(x => x.Member.Email)
				.Must(email => !dbContext.Members.Any(m => m.Email == email))
				.WithMessage(x => $"Email [email: {x.Member.Email}] already in use");

			RuleFor(x => x.Member.PhoneNumber)
				.Matches(
					"^\\d{9}$");

			RuleFor(x => x.Member.Pesel)
				.NotEmpty()
				.Matches("^\\d{11}$");

			RuleFor(x => x.Member.Pesel)
				.Must(pesel => !dbContext.Members.Any(m => m.Pesel == pesel))
				.WithMessage(x => $"Pesel [pesel: {x.Member.Pesel}] already in use");
		}
	}
}