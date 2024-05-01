using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class addattributestorequesttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AttendedAt",
                table: "Requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompletedAt",
                table: "Requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendedAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Requests");
        }
    }
}
