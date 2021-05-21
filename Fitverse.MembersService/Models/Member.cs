using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitverse.MembersService.Models
{
	public class Member
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MemberId { get; private set; }

		[Required] public string Name { get; set; }

		[Required] public string SurName { get; set; }

		[Column(TypeName = "Date")] public DateTime BirthDate { get; set; }

		[Required] public string Email { get; set; }

		public string PhoneNumber { get; set; }

		[Required] public string Pesel { get; set; }
	}
}