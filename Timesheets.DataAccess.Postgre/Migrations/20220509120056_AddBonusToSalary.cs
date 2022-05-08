using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.DataAccess.Postgre.Migrations
{
    /// <inheritdoc />
    public partial class AddBonusToSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Bonus",
                table: "Salaries",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "Salaries");
        }
    }
}