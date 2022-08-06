using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.DataAccess.Postgre.Migrations
{
    public partial class AddUniqueChatId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TelegramUsers_ChatId",
                table: "TelegramUsers",
                column: "ChatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TelegramUsers_ChatId",
                table: "TelegramUsers");
        }
    }
}
