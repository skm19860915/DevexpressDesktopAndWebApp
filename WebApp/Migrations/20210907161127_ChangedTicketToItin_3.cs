using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class ChangedTicketToItin_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Leg_QuoteRequestTicketID",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_QuoteRequestTicketID",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "QuoteRequestTicketID",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "TripQuoteID",
                table: "Transportations");

            migrationBuilder.AddColumn<int>(
                name: "FlightItinId",
                table: "Transportations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LegGUID",
                table: "Transportations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegId",
                table: "Transportations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_FlightItinId",
                table: "Transportations",
                column: "FlightItinId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_LegId",
                table: "Transportations",
                column: "LegId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_FlightItineraries_FlightItinId",
                table: "Transportations",
                column: "FlightItinId",
                principalTable: "FlightItineraries",
                principalColumn: "FlightItineraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Leg_LegId",
                table: "Transportations",
                column: "LegId",
                principalTable: "Leg",
                principalColumn: "LegID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_FlightItineraries_FlightItinId",
                table: "Transportations");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Leg_LegId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_FlightItinId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_LegId",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "FlightItinId",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "LegGUID",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "LegId",
                table: "Transportations");

            migrationBuilder.AddColumn<int>(
                name: "QuoteRequestTicketID",
                table: "Transportations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TripQuoteID",
                table: "Transportations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_QuoteRequestTicketID",
                table: "Transportations",
                column: "QuoteRequestTicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Leg_QuoteRequestTicketID",
                table: "Transportations",
                column: "QuoteRequestTicketID",
                principalTable: "Leg",
                principalColumn: "LegID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
