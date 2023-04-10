using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId1",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId2",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_ContactId1",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_ContactId2",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "ContactId1",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "ContactId2",
                table: "MemberShips");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Relationships",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfMemberShip",
                table: "MemberShips",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "TypeOfMemberShip",
                table: "MemberShips");

            migrationBuilder.AddColumn<string>(
                name: "ContactId1",
                table: "MemberShips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId2",
                table: "MemberShips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_ContactId1",
                table: "MemberShips",
                column: "ContactId1");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_ContactId2",
                table: "MemberShips",
                column: "ContactId2");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId1",
                table: "MemberShips",
                column: "ContactId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId2",
                table: "MemberShips",
                column: "ContactId2",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
