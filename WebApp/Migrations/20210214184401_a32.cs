using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CommissionRate",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fee",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FranchiseOwnerId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_FranchiseOwnerId",
                table: "Companies",
                column: "FranchiseOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Contact_FranchiseOwnerId",
                table: "Companies",
                column: "FranchiseOwnerId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Contact_FranchiseOwnerId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_FranchiseOwnerId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FranchiseOwnerId",
                table: "Companies");
        }
    }
}
