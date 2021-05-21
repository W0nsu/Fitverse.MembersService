namespace Fitverse.MembersService.Dtos
{
	public class ReservationDto
	{
		public int Id { get; private set; }

		public int ReservationId { get; set; }

		public int ClassId { get; set; }

		public int MemberId { get; set; }
	}
}