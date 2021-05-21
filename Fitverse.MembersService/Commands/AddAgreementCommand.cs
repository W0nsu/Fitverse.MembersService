using Fitverse.MembersService.Dtos;
using MediatR;

namespace Fitverse.MembersService.Commands
{
	public class AddAgreementCommand : IRequest<AgreementDto>
	{
		public AddAgreementCommand(AgreementDto agreement)
		{
			Agreement = agreement;
		}

		public AgreementDto Agreement { get; }
	}
}