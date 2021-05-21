using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitverse.MembersService.Models
{
	public class Agreement
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AgreementId { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(30)]
		public string Name { get; set; }

		[Required] public int MembershipId { get; set; }

		[Required] public int MemberId { get; set; }

		[Required] [Column(TypeName = "Date")] public DateTime StartingDate { get; set; }

		[Required] public bool IsPaid { get; set; }
	}
}