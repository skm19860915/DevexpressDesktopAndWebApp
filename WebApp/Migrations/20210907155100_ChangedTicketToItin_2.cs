using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class ChangedTicketToItin_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_Leg_InBoundLegID",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_Leg_OutBoundLegID",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_Quotes_QuoteID",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_QuoteRequests_QuoteRequestID",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_Companies_TourOperatorId",
                table: "QuoteRequestTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteRequestTickets_FlightItineraryId",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequestTickets",
                table: "QuoteRequestTickets");

            migrationBuilder.RenameTable(
                name: "QuoteRequestTickets",
                newName: "FlightItineraries");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_TourOperatorId",
                table: "FlightItineraries",
                newName: "IX_FlightItineraries_TourOperatorId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_QuoteRequestID",
                table: "FlightItineraries",
                newName: "IX_FlightItineraries_QuoteRequestID");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_QuoteID",
                table: "FlightItineraries",
                newName: "IX_FlightItineraries_QuoteID");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_QuoteGroupId",
                table: "FlightItineraries",
                newName: "IX_FlightItineraries_QuoteGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_OutBoundLegID",
                table: "FlightItineraries",
                newName: "IX_FlightItineraries_OutBoundLegID");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_InBoundLegID",
                table: "FlightItineraries",
                newName: "IX_FlightItineraries_InBoundLegID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightItineraries",
                table: "FlightItineraries",
                column: "FlightItineraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightItineraries_Leg_InBoundLegID",
                table: "FlightItineraries",
                column: "InBoundLegID",
                principalTable: "Leg",
                principalColumn: "LegID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightItineraries_Leg_OutBoundLegID",
                table: "FlightItineraries",
                column: "OutBoundLegID",
                principalTable: "Leg",
                principalColumn: "LegID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightItineraries_QuoteGroups_QuoteGroupId",
                table: "FlightItineraries",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightItineraries_Quotes_QuoteID",
                table: "FlightItineraries",
                column: "QuoteID",
                principalTable: "Quotes",
                principalColumn: "QuoteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightItineraries_QuoteRequests_QuoteRequestID",
                table: "FlightItineraries",
                column: "QuoteRequestID",
                principalTable: "QuoteRequests",
                principalColumn: "QuoteRequestID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightItineraries_Companies_TourOperatorId",
                table: "FlightItineraries",
                column: "TourOperatorId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteGroups_FlightItineraries_QuoteRequestTicketId",
                table: "QuoteGroups",
                column: "QuoteRequestTicketId",
                principalTable: "FlightItineraries",
                principalColumn: "FlightItineraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteToResultsMappers_FlightItineraries_FlightItineraryId",
                table: "QuoteToResultsMappers",
                column: "FlightItineraryId",
                principalTable: "FlightItineraries",
                principalColumn: "FlightItineraryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightItineraries_Leg_InBoundLegID",
                table: "FlightItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightItineraries_Leg_OutBoundLegID",
                table: "FlightItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightItineraries_QuoteGroups_QuoteGroupId",
                table: "FlightItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightItineraries_Quotes_QuoteID",
                table: "FlightItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightItineraries_QuoteRequests_QuoteRequestID",
                table: "FlightItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightItineraries_Companies_TourOperatorId",
                table: "FlightItineraries");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteGroups_FlightItineraries_QuoteRequestTicketId",
                table: "QuoteGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteToResultsMappers_FlightItineraries_FlightItineraryId",
                table: "QuoteToResultsMappers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightItineraries",
                table: "FlightItineraries");

            migrationBuilder.RenameTable(
                name: "FlightItineraries",
                newName: "QuoteRequestTickets");

            migrationBuilder.RenameIndex(
                name: "IX_FlightItineraries_TourOperatorId",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_TourOperatorId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightItineraries_QuoteRequestID",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_QuoteRequestID");

            migrationBuilder.RenameIndex(
                name: "IX_FlightItineraries_QuoteID",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_QuoteID");

            migrationBuilder.RenameIndex(
                name: "IX_FlightItineraries_QuoteGroupId",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_QuoteGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightItineraries_OutBoundLegID",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_OutBoundLegID");

            migrationBuilder.RenameIndex(
                name: "IX_FlightItineraries_InBoundLegID",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_InBoundLegID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequestTickets",
                table: "QuoteRequestTickets",
                column: "FlightItineraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups",
                column: "QuoteRequestTicketId",
                principalTable: "QuoteRequestTickets",
                principalColumn: "FlightItineraryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_Leg_InBoundLegID",
                table: "QuoteRequestTickets",
                column: "InBoundLegID",
                principalTable: "Leg",
                principalColumn: "LegID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_Leg_OutBoundLegID",
                table: "QuoteRequestTickets",
                column: "OutBoundLegID",
                principalTable: "Leg",
                principalColumn: "LegID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_Quotes_QuoteID",
                table: "QuoteRequestTickets",
                column: "QuoteID",
                principalTable: "Quotes",
                principalColumn: "QuoteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_QuoteRequests_QuoteRequestID",
                table: "QuoteRequestTickets",
                column: "QuoteRequestID",
                principalTable: "QuoteRequests",
                principalColumn: "QuoteRequestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_Companies_TourOperatorId",
                table: "QuoteRequestTickets",
                column: "TourOperatorId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteToResultsMappers_QuoteRequestTickets_FlightItineraryId",
                table: "QuoteToResultsMappers",
                column: "FlightItineraryId",
                principalTable: "QuoteRequestTickets",
                principalColumn: "FlightItineraryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
