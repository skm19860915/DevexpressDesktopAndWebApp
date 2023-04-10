using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class cr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditId",
                table: "Booking",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    When = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ContactId = table.Column<string>(nullable: true),
                    OriginalBookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credits_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Credits_Booking_OriginalBookingId",
                        column: x => x.OriginalBookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreditId",
                table: "Booking",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_ContactId",
                table: "Credits",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_OriginalBookingId",
                table: "Credits",
                column: "OriginalBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Credits_CreditId",
                table: "Booking",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Credits_CreditId",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CreditId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CreditId",
                table: "Booking");
        }
    }
}
