using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v67 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilteredAccommodationId",
                table: "FilteredAccommodations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilteredAccommodationId",
                table: "AgentAirPortPreferences",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilteredAccommodationId",
                table: "FilteredAccommodations");

            migrationBuilder.DropColumn(
                name: "FilteredAccommodationId",
                table: "AgentAirPortPreferences");
        }
    }
}
