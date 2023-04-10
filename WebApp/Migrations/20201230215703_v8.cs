using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "ContactSubTypeId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContactTypeId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactSubTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactTypeId = table.Column<int>(nullable: false),
                    AgentID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSubTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactSubTypes_ContactTypes_AgentID",
                        column: x => x.AgentID,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ContactSubTypeId",
                table: "AppUsers",
                column: "ContactSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ContactTypeId",
                table: "AppUsers",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactSubTypes_AgentID",
                table: "ContactSubTypes",
                column: "AgentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_ContactSubTypes_ContactSubTypeId",
                table: "AppUsers",
                column: "ContactSubTypeId",
                principalTable: "ContactSubTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_ContactTypes_ContactTypeId",
                table: "AppUsers",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_ContactSubTypes_ContactSubTypeId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_ContactTypes_ContactTypeId",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "ContactSubTypes");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ContactSubTypeId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ContactTypeId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ContactSubTypeId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ContactTypeId",
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
        }
    }
}
