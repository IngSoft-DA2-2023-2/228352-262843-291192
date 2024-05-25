using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class associationTableForContructionCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyAdminAssociations",
                columns: table => new
                {
                    ConstructionCompanyAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConstructionCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAdminAssociations", x => new { x.ConstructionCompanyAdminId, x.ConstructionCompanyId });
                    table.ForeignKey(
                        name: "FK_CompanyAdminAssociations_ConstructionCompanies_ConstructionCompanyId",
                        column: x => x.ConstructionCompanyId,
                        principalTable: "ConstructionCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyAdminAssociations_Users_ConstructionCompanyAdminId",
                        column: x => x.ConstructionCompanyAdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAdminAssociations_ConstructionCompanyId",
                table: "CompanyAdminAssociations",
                column: "ConstructionCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyAdminAssociations");
        }
    }
}
