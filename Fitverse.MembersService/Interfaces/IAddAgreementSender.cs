using Fitverse.MembersService.Models;

namespace Fitverse.MembersService.Interfaces
{
	public interface IAddAgreementSender
	{
		public void SendAgreement(Agreement agreement);
	}
}