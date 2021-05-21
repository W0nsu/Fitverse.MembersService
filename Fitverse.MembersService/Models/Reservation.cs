using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitverse.MembersService.Models
{
	public class Reservation
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required] public int ReservationId { get; set; }

		[Required] public int ClassId { get; set; }

		[Required] public int MemberId { get; set; }
	}
}