using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class ChangedTicketToItin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteRequestTickets_QuoteRequestTicketID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropIndex(
                name: "IX_QuoteToResultsMappers_QuoteRequestTicketID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequestTickets",
                table: "QuoteRequestTickets");

            migrationBuilder.DropColumn(
                name: "QuoteRequestTicketID",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "QuoteRequestTickets");

            migrationBuilder.AddColumn<int>(
                name: "FlightItineraryId",
                table: "QuoteToResultsMappers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightItineraryId",
                table: "QuoteRequestTickets",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequestTickets",
                table: "QuoteRequestTickets",
                column: "FlightItineraryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteToResultsMappers_FlightItineraryId",
                table: "QuoteToResultsMappers",
                column: "FlightItineraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups",
                column: "QuoteRequestTicketId",
                principalTable: "QuoteRequestTickets",
                principalColumn: "FlightItineraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteRequestTickets_FlightItineraryId",
                table: "QuoteToResultsMappers",
                column: "FlightItineraryId",
                principalTable: "QuoteRequestTickets",
                principalColumn: "FlightItineraryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteRequestTickets_FlightItineraryId",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropIndex(
                name: "IX_QuoteToResultsMappers_FlightItineraryId",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequestTickets",
                table: "QuoteRequestTickets");

            migrationBuilder.DropColumn(
                name: "FlightItineraryId",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropColumn(
                name: "FlightItineraryId",
                table: "QuoteRequestTickets");

            migrationBuilder.AddColumn<int>(
                name: "QuoteRequestTicketID",
                table: "QuoteToResultsMappers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketID",
                table: "QuoteRequestTickets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequestTickets",
                table: "QuoteRequestTickets",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteToResultsMappers_QuoteRequestTicketID",
                table: "QuoteToResultsMappers",
                column: "QuoteRequestTicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups",
                column: "QuoteRequestTicketId",
                principalTable: "QuoteRequestTickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteRequestTickets_QuoteRequestTicketID",
                table: "QuoteToResultsMappers",
                column: "QuoteRequestTicketID",
                principalTable: "QuoteRequestTickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
