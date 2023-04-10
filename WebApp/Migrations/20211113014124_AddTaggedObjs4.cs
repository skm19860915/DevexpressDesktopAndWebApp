using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddTaggedObjs4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presentations_WebPages_CurrentWebPageId",
                table: "Presentations");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentWebPageId",
                table: "Presentations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Presentations_WebPages_CurrentWebPageId",
                table: "Presentations",
                column: "CurrentWebPageId",
                principalTable: "WebPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presentations_WebPages_CurrentWebPageId",
                table: "Presentations");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentWebPageId",
                table: "Presentations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Presentations_WebPages_CurrentWebPageId",
                table: "Presentations",
                column: "CurrentWebPageId",
                principalTable: "WebPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
