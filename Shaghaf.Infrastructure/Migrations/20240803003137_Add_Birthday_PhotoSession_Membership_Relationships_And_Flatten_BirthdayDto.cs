using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Birthday_PhotoSession_Membership_Relationships_And_Flatten_BirthdayDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birthdays_Homes_HomeId",
                table: "Birthdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Homes_HomeId",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions");

            migrationBuilder.DropTable(
                name: "AdditionalItems");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_HomeId",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_HomeId",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Birthdays");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PhotoSessions",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PhotoSessions",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Memberships",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "HomeId",
                table: "Memberships",
                newName: "MaxGuests");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Birthdays",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "HomeId",
                table: "Birthdays",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Birthdays_HomeId",
                table: "Birthdays",
                newName: "IX_Birthdays_RoomId");

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BirthdayId",
                table: "PhotoSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "PhotoSessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "PhotoSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "Memberships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirthdayId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoSessionId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfGuests",
                table: "Birthdays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_MembershipId",
                table: "Rooms",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_BirthdayId",
                table: "PhotoSessions",
                column: "BirthdayId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_RoomId",
                table: "PhotoSessions",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BirthdayId",
                table: "Bookings",
                column: "BirthdayId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PhotoSessionId",
                table: "Bookings",
                column: "PhotoSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birthdays_Rooms_RoomId",
                table: "Birthdays",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Birthdays_BirthdayId",
                table: "Bookings",
                column: "BirthdayId",
                principalTable: "Birthdays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_PhotoSessions_PhotoSessionId",
                table: "Bookings",
                column: "PhotoSessionId",
                principalTable: "PhotoSessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Birthdays_BirthdayId",
                table: "PhotoSessions",
                column: "BirthdayId",
                principalTable: "Birthdays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Rooms_RoomId",
                table: "PhotoSessions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Memberships_MembershipId",
                table: "Rooms",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birthdays_Rooms_RoomId",
                table: "Birthdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Birthdays_BirthdayId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_PhotoSessions_PhotoSessionId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Birthdays_BirthdayId",
                table: "PhotoSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Rooms_RoomId",
                table: "PhotoSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Memberships_MembershipId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_MembershipId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_BirthdayId",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_RoomId",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BirthdayId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PhotoSessionId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BirthdayId",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "BirthdayId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PhotoSessionId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "NumberOfGuests",
                table: "Birthdays");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "PhotoSessions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "PhotoSessions",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Memberships",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "MaxGuests",
                table: "Memberships",
                newName: "HomeId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Birthdays",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Birthdays",
                newName: "HomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Birthdays_RoomId",
                table: "Birthdays",
                newName: "IX_Birthdays_HomeId");

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "PhotoSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Birthdays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AdditionalItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalItems_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_HomeId",
                table: "PhotoSessions",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_HomeId",
                table: "Memberships",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalItems_BookingId",
                table: "AdditionalItems",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birthdays_Homes_HomeId",
                table: "Birthdays",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Homes_HomeId",
                table: "Memberships",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
