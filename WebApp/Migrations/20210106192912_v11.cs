using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_ContactId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AgentID",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ContactId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "AppUsers",
                newName: "AgentId");

            migrationBuilder.AddColumn<bool>(
                name: "Primary",
                table: "UserMaps",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AssistantId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PreferredAirPort",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirPortID = table.Column<int>(nullable: false),
                    ContactId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferredAirPort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferredAirPort_AirPort_AirPortID",
                        column: x => x.AirPortID,
                        principalTable: "AirPort",
                        principalColumn: "AirPortID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferredAirPort_AppUsers_ContactId",
                        column: x => x.ContactId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AssistantId",
                table: "AppUsers",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ClientId",
                table: "AppUsers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferredAirPort_AirPortID",
                table: "PreferredAirPort",
                column: "AirPortID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferredAirPort_ContactId",
                table: "PreferredAirPort",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_AssistantId",
                table: "AppUsers",
                column: "AssistantId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_ClientId",
                table: "AppUsers",
                column: "ClientId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUsers_AgentID",
                table: "AppUsers",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AgentId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AssistantId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_ClientId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUsers_AgentID",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "PreferredAirPort");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AssistantId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ClientId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AgentID",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Primary",
                table: "UserMaps");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AgentID",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "AppUsers",
                newName: "AgentID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_AgentId",
                table: "AppUsers",
                newName: "IX_AppUsers_AgentID");

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
                name: "ContactId",
                table: "AppUsers",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "FK_AppUsers_AppUsers_AgentID",
                table: "AppUsers",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
