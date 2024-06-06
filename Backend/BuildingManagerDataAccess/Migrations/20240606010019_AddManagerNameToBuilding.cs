using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class AddManagerNameToBuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manager",
                table: "Buildings");
        }
    }
}
