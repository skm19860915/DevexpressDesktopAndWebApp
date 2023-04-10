using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class sp4Quotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuoteID",
                table: "QuoteRequestTickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestTickets_QuoteID",
                table: "QuoteRequestTickets",
                column: "QuoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_Quotes_QuoteID",
                table: "QuoteRequestTickets",
                column: "QuoteID",
                principalTable: "Quotes",
                principalColumn: "QuoteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_Quotes_QuoteID",
                table: "QuoteRequestTickets");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequestTickets_QuoteID",
                table: "QuoteRequestTickets");

            migrationBuilder.DropColumn(
                name: "QuoteID",
                table: "QuoteRequestTickets");
        }
    }
}
