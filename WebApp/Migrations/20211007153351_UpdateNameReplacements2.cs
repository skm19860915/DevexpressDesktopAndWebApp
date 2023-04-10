using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class UpdateNameReplacements2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SKUId",
                table: "NameReplacements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NameReplacements_SKUId",
                table: "NameReplacements",
                column: "SKUId");

            migrationBuilder.AddForeignKey(
                name: "FK_NameReplacements_SKUs_SKUId",
                table: "NameReplacements",
                column: "SKUId",
                principalTable: "SKUs",
                principalColumn: "SKUID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NameReplacements_SKUs_SKUId",
                table: "NameReplacements");

            migrationBuilder.DropIndex(
                name: "IX_NameReplacements_SKUId",
                table: "NameReplacements");

            migrationBuilder.DropColumn(
                name: "SKUId",
                table: "NameReplacements");
        }
    }
}
