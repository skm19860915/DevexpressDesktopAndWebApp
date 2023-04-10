using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class UpdateNameReplacements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewName",
                table: "NameReplacements");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "NameReplacements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NameReplacements_HotelId",
                table: "NameReplacements",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_NameReplacements_Companies_HotelId",
                table: "NameReplacements",
                column: "HotelId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NameReplacements_Companies_HotelId",
                table: "NameReplacements");

            migrationBuilder.DropIndex(
                name: "IX_NameReplacements_HotelId",
                table: "NameReplacements");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "NameReplacements");

            migrationBuilder.AddColumn<string>(
                name: "NewName",
                table: "NameReplacements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
