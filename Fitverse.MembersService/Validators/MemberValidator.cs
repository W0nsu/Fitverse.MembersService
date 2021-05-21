using System;
using System.Linq;
using Fitverse.MembersService.Data;
using Fitverse.MembersService.Models;
using FluentValidation;

namespace Fitverse.MembersService.Validators
{
	public class MemberValidator : AbstractValidator<Member>
	{
		public MemberValidator(MembersContext dbContext, Tuple<Member, Member> memberBeforeAndAfterChange)
		{
			Member memberBeforeChange, memberAfterChange;
			(memberBeforeChange, memberAfterChange) = memberBeforeAndAfterChange;
	
			RuleFor(x => x.Name)
				.NotEmpty();
	
			RuleFor(x => x.SurName)
				.NotEmpty();
	
			RuleFor(x => x.Email)
				.EmailAddress();
	
			if (memberBeforeChange.Email != memberAfterChange.Email)
			{
				RuleFor(x => x.Email)
					.Must(email => !dbContext.Members.Any(m => m.Email == email))
					.WithMessage(x => $"Email [email: {x.Email}] already in use");
			}
	
			RuleFor(x => x.PhoneNumber)
				.Matches(
					"^\\d{9}$");
	
			RuleFor(x => x.Pesel)
				.NotEmpty()
				.Matches("^\\d{11}$");
	
			if (memberBeforeChange.Pesel != memberAfterChange.Pesel)
			{
				RuleFor(x => x.Pesel)
					.Must(pesel => !dbContext.Members.Any(m => m.Pesel == pesel))
					.WithMessage(x => $"Pesel [pesel: {x.Pesel}] already in use");
			}
		}
	}
}