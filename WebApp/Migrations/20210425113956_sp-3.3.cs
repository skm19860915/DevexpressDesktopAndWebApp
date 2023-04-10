using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class sp33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AirportDistance",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Promo",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirportDistance",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Promo",
                table: "Companies");
        }
    }
}
