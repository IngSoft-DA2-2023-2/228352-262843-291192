using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class adddeletecascadelogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Owners_OwnerEmail",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MaintainerStaffId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Owners_OwnerEmail",
                table: "Apartments",
                column: "OwnerEmail",
                principalTable: "Owners",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Buildings_BuildingId",
                table: "Requests",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MaintainerStaffId",
                table: "Requests",
                column: "MaintainerStaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Owners_OwnerEmail",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Buildings_BuildingId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MaintainerStaffId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Owners_OwnerEmail",
                table: "Apartments",
                column: "OwnerEmail",
                principalTable: "Owners",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MaintainerStaffId",
                table: "Requests",
                column: "MaintainerStaffId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
