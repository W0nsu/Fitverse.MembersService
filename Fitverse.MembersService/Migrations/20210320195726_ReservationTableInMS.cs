using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.MembersService.Migrations
{
	public partial class ReservationTableInMS : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				"Pesel",
				"Members",
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(20,0)");

			migrationBuilder.CreateTable(
				"ReservationDto",
				table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ReservationId = table.Column<int>(nullable: false),
					ClassId = table.Column<int>(nullable: false),
					MemberId = table.Column<int>(nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_ReservationDto", x => x.Id); });

			migrationBuilder.CreateTable(
				"Reservations",
				table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ReservationId = table.Column<int>(nullable: false),
					ClassId = table.Column<int>(nullable: false),
					MemberId = table.Column<int>(nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_Reservations", x => x.Id); });

			migrationBuilder.CreateIndex(
				"IX_ReservationDto_ReservationId",
				"ReservationDto",
				"ReservationId",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"ReservationDto");

			migrationBuilder.DropTable(
				"Reservations");

			migrationBuilder.AlterColumn<decimal>(
				"Pesel",
				"Members",
				"decimal(20,0)",
				nullable: false,
				oldClrType: typeof(string));
		}
	}
}