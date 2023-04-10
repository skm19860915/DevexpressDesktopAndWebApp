using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class UpdateNameReplacements3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NameReplacements_Companies_TourOperatorId",
                table: "NameReplacements");

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorId",
                table: "NameReplacements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_NameReplacements_Companies_TourOperatorId",
                table: "NameReplacements",
                column: "TourOperatorId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NameReplacements_Companies_TourOperatorId",
                table: "NameReplacements");

            migrationBuilder.AlterColumn<int>(
                name: "TourOperatorId",
                table: "NameReplacements",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NameReplacements_Companies_TourOperatorId",
                table: "NameReplacements",
                column: "TourOperatorId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
