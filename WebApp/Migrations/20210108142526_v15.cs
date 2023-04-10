using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AgentId2",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AgentId2",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AgentId2",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AgentId",
                table: "AppUsers",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_AgentId",
                table: "AppUsers",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AgentId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AgentId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "AppUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "AgentId2",
                table: "AppUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AgentId2",
                table: "AppUsers",
                column: "AgentId2");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_AgentId2",
                table: "AppUsers",
                column: "AgentId2",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
