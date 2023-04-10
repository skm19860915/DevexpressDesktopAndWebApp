using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddChildPriceToStaging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChildPrice",
                table: "Staging_Hotels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChildPrice",
                table: "Staging_HotelRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "QuoteRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildPrice",
                table: "Staging_Hotels");

            migrationBuilder.DropColumn(
                name: "ChildPrice",
                table: "Staging_HotelRates");

            migrationBuilder.DropColumn(
                name: "NumberOfChildren",
                table: "QuoteRequests");
        }
    }
}
