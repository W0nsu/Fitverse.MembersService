using Fitverse.MembersService.Models;

namespace Fitverse.MembersService.Interfaces
{
	public interface IAddMemberSender
	{
		public void SendMember(Member member);
	}
}