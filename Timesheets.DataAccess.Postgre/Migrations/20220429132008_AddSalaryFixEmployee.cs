using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.DataAccess.Postgre.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaryFixEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Position = table.Column<int>(type: "integer", nullable: false),
                    MonthSalary = table.Column<int>(type: "integer", nullable: false),
                    MonthBonus = table.Column<int>(type: "integer", nullable: false),
                    SalaryPerHour = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Position);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_Position",
                        column: x => x.Position,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
