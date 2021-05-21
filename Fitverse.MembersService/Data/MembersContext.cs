using Fitverse.MembersService.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.MembersService.Data
{
	public class MembersContext : DbContext
	{
		public MembersContext(DbContextOptions<MembersContext> options)
			: base(options)
		{
		}

		public DbSet<Member> Members { get; set; }

		public DbSet<Membership> Memberships { get; set; }

		public DbSet<Agreement> Agreements { get; set; }

		public DbSet<Reservation> Reservations { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Membership>()
				.HasIndex(u => u.MembershipId)
				.IsUnique();

			builder.Entity<Reservation>()
				.HasIndex(u => u.ReservationId)
				.IsUnique();
		}
	}
}