using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditOnPhotosession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Birthdays_BirthdayId",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_BirthdayId",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "BirthdayId",
                table: "PhotoSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BirthdayId",
                table: "PhotoSessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_BirthdayId",
                table: "PhotoSessions",
                column: "BirthdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Birthdays_BirthdayId",
                table: "PhotoSessions",
                column: "BirthdayId",
                principalTable: "Birthdays",
                principalColumn: "Id");
        }
    }
}
