using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class companyidinbuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConstructionCompany",
                table: "Buildings");

            migrationBuilder.AddColumn<Guid>(
                name: "ConstructionCompanyId",
                table: "Buildings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ConstructionCompanyId",
                table: "Buildings",
                column: "ConstructionCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ConstructionCompanies_ConstructionCompanyId",
                table: "Buildings",
                column: "ConstructionCompanyId",
                principalTable: "ConstructionCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ConstructionCompanies_ConstructionCompanyId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_ConstructionCompanyId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "ConstructionCompanyId",
                table: "Buildings");

            migrationBuilder.AddColumn<string>(
                name: "ConstructionCompany",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
