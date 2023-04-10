using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AppUserId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_AppUserId",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_AppUserId1",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_AppUserId2",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_AppUserId",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_AppUserId1",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_AppUserId2",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AppUserId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "AppUserId2",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "MemberShips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId1",
                table: "MemberShips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId2",
                table: "MemberShips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_ContactId",
                table: "MemberShips",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_ContactId1",
                table: "MemberShips",
                column: "ContactId1");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_ContactId2",
                table: "MemberShips",
                column: "ContactId2");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ContactId",
                table: "AppUsers",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_ContactId",
                table: "AppUsers",
                column: "ContactId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId",
                table: "MemberShips",
                column: "ContactId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_ContactId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId1",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId2",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_ContactId",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_ContactId1",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_ContactId2",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ContactId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "ContactId1",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "ContactId2",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "MemberShips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "MemberShips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId2",
                table: "MemberShips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "AppUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_AppUserId",
                table: "MemberShips",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_AppUserId1",
                table: "MemberShips",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_AppUserId2",
                table: "MemberShips",
                column: "AppUserId2");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AppUserId",
                table: "AppUsers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_AppUserId",
                table: "AppUsers",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_AppUsers_AppUserId",
                table: "MemberShips",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_AppUsers_AppUserId1",
                table: "MemberShips",
                column: "AppUserId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_AppUsers_AppUserId2",
                table: "MemberShips",
                column: "AppUserId2",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
