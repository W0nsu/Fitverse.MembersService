using System;

namespace Fitverse.MembersService.Dtos
{
	public class AgreementDto
	{
		public int AgreementId { get; private set; }

		public string Name { get; set; }
		public int MembershipId { get; set; }
		public int MemberId { get; set; }
		public DateTime StartingDate { get; set; }

		public bool IsPaid { get; private set; }
	}
}