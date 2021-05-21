using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.MembersService.Migrations
{
	public partial class ReservationTableInMSFix : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"ReservationDto");

			migrationBuilder.CreateIndex(
				"IX_Reservations_ReservationId",
				"Reservations",
				"ReservationId",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				"IX_Reservations_ReservationId",
				"Reservations");

			migrationBuilder.CreateTable(
				"ReservationDto",
				table => new
				{
					Id = table.Column<int>("int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ClassId = table.Column<int>("int", nullable: false),
					MemberId = table.Column<int>("int", nullable: false),
					ReservationId = table.Column<int>("int", nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_ReservationDto", x => x.Id); });

			migrationBuilder.CreateIndex(
				"IX_ReservationDto_ReservationId",
				"ReservationDto",
				"ReservationId",
				unique: true);
		}
	}
}