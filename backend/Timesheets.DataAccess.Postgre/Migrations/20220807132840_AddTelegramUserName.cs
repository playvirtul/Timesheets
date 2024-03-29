﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.DataAccess.Postgre.Migrations
{
    public partial class AddTelegramUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelegramUserName",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramUserName",
                table: "Users");
        }
    }
}
