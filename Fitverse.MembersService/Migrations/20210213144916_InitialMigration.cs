using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitverse.MembersService.Migrations
{
	public partial class InitialMigration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"Agreements",
				table => new
				{
					AgreementId = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(maxLength: 30, nullable: false),
					MembershipId = table.Column<int>(nullable: false),
					MemberId = table.Column<int>(nullable: false),
					StartingDate = table.Column<DateTime>("Date", nullable: false),
					IsPaid = table.Column<bool>(nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_Agreements", x => x.AgreementId); });

			migrationBuilder.CreateTable(
				"Members",
				table => new
				{
					MemberId = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(nullable: false),
					SurName = table.Column<string>(nullable: false),
					BirthDate = table.Column<DateTime>("Date", nullable: false),
					Email = table.Column<string>(nullable: false),
					PhoneNumber = table.Column<string>(nullable: true),
					Pesel = table.Column<decimal>(nullable: false),
					Password = table.Column<string>(nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_Members", x => x.MemberId); });

			migrationBuilder.CreateTable(
				"Memberships",
				table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					MembershipId = table.Column<int>(nullable: false),
					Name = table.Column<string>(maxLength: 30, nullable: false),
					PeriodType = table.Column<int>(nullable: false),
					Duration = table.Column<int>(nullable: false),
					TerminationPeriod = table.Column<int>(nullable: false),
					InstallmentPrice = table.Column<float>(nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_Memberships", x => x.Id); });

			migrationBuilder.CreateIndex(
				"IX_Memberships_MembershipId",
				"Memberships",
				"MembershipId",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"Agreements");

			migrationBuilder.DropTable(
				"Members");

			migrationBuilder.DropTable(
				"Memberships");
		}
	}
}