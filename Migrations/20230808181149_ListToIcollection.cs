using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightAPI.Migrations
{
    /// <inheritdoc />
    public partial class ListToIcollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UsersUserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UsersUserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UsersUserId",
                table: "Bookings",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UsersUserId",
                table: "Bookings",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
