using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class managerIdisfkinbuildingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ManagerId",
                table: "Buildings",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Users_ManagerId",
                table: "Buildings",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Users_ManagerId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_ManagerId",
                table: "Buildings");
        }
    }
}
