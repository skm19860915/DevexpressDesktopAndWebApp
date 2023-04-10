using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Companies_HotelId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_HotelId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Booking");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SupplierId",
                table: "Booking",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Companies_SupplierId",
                table: "Booking",
                column: "SupplierId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Companies_SupplierId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_SupplierId",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_HotelId",
                table: "Booking",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Companies_HotelId",
                table: "Booking",
                column: "HotelId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
