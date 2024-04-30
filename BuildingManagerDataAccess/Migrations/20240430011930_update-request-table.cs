using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class updaterequesttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerId1",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CategoryId1",
                table: "Requests",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_MaintainerId1",
                table: "Requests",
                column: "MaintainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Categories_CategoryId1",
                table: "Requests",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MaintainerId1",
                table: "Requests",
                column: "MaintainerId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Categories_CategoryId1",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MaintainerId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CategoryId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_MaintainerId1",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "MaintainerId1",
                table: "Requests");
        }
    }
}
