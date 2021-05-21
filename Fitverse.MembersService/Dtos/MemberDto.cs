using System;

namespace Fitverse.MembersService.Dtos
{
	public class MemberDto
	{
		public int MemberId { get; private set; }
		public string Name { get; set; }
		public string SurName { get; set; }

		public DateTime BirthDate { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Pesel { get; set; }
	}
}