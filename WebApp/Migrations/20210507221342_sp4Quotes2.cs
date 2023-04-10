using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class sp4Quotes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_QuoteRequests_QuoteID",
                table: "Transportations");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Quotes_QuoteID",
                table: "Transportations",
                column: "QuoteID",
                principalTable: "Quotes",
                principalColumn: "QuoteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Quotes_QuoteID",
                table: "Transportations");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_QuoteRequests_QuoteID",
                table: "Transportations",
                column: "QuoteID",
                principalTable: "QuoteRequests",
                principalColumn: "QuoteRequestID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
