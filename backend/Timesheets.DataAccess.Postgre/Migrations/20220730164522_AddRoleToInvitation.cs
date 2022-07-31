using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.DataAccess.Postgre.Migrations
{
    public partial class AddRoleToInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Invitations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Invitations");
        }
    }
}
