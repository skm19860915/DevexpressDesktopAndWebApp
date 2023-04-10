using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssuerID",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IssuerID",
                table: "Tasks",
                column: "IssuerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Contact_IssuerID",
                table: "Tasks",
                column: "IssuerID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Contact_IssuerID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_IssuerID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IssuerID",
                table: "Tasks");
        }
    }
}
