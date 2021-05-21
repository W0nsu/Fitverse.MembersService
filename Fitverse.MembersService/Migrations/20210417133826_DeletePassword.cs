using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.MembersService.Migrations
{
	public partial class DeletePassword : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				"Password",
				"Members");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				"Password",
				"Members",
				"nvarchar(max)",
				nullable: false,
				defaultValue: "");
		}
	}
}