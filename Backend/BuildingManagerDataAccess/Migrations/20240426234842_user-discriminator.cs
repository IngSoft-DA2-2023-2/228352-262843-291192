using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagerDataAccess.Migrations
{
    public partial class userdiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Users",
                newName: "user_type");

            migrationBuilder.InsertData(
                               table: "Users",
                               columns: new[] { "Id", "Email", "Name", "Lastname", "Password", "Role", "user_type" },
                               values: new object[] { Guid.NewGuid(), "admin@admin.com", "Homero", "Simpson", "password", 0, "admin_user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_type",
                table: "Users",
                newName: "Discriminator");
        }
    }
}
