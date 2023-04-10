using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobelEntryNumber",
                table: "Contact");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Contact",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GlobalEntryNumber",
                table: "Contact",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "GlobalEntryNumber",
                table: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "GlobelEntryNumber",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
