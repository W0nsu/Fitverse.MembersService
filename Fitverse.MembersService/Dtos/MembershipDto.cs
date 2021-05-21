namespace Fitverse.MembersService.Dtos
{
	public class MembershipDto
	{
		public int Id { get; private set; }

		public int MembershipId { get; set; }

		public string Name { get; set; }

		public int PeriodType { get; set; }

		public int Duration { get; set; }

		public int TerminationPeriod { get; set; }

		public float InstallmentPrice { get; set; }
	}
}