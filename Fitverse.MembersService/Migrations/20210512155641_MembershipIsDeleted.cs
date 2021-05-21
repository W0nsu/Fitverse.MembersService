using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.MembersService.Migrations
{
	public partial class MembershipIsDeleted : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				"IsDeleted",
				"Memberships",
				nullable: false,
				defaultValue: false);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				"IsDeleted",
				"Memberships");
		}
	}
}