using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class fixrequesttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Categories_CategoryId1",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MaintainerId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MaintainerId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CategoryId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_MaintainerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "MaintainerId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "MaintainerId1",
                table: "Requests",
                newName: "MaintainerStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_MaintainerId1",
                table: "Requests",
                newName: "IX_Requests_MaintainerStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MaintainerStaffId",
                table: "Requests",
                column: "MaintainerStaffId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MaintainerStaffId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "MaintainerStaffId",
                table: "Requests",
                newName: "MaintainerId1");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_MaintainerStaffId",
                table: "Requests",
                newName: "IX_Requests_MaintainerId1");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CategoryId1",
                table: "Requests",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_MaintainerId",
                table: "Requests",
                column: "MaintainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Categories_CategoryId1",
                table: "Requests",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MaintainerId",
                table: "Requests",
                column: "MaintainerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MaintainerId1",
                table: "Requests",
                column: "MaintainerId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
