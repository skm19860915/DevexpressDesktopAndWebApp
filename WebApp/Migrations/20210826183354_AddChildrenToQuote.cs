using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddChildrenToQuote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Child1Age",
                table: "QuoteRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Child2Age",
                table: "QuoteRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Child3Age",
                table: "QuoteRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Child1Age",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Child2Age",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "Child3Age",
                table: "QuoteRequests");
        }
    }
}
