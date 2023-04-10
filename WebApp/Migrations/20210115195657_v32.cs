using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "QuoteRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Opportunities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnedById",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssistantId",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Contact",
                nullable: true);

 
            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_AgentId",
                table: "QuoteRequests",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AssistantId",
                table: "Contact",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ClientId",
                table: "Contact",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Contact_OwnedById",
                table: "Contact",
                column: "OwnedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Contact_AssistantId",
                table: "Contact",
                column: "AssistantId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Contact_ClientId",
                table: "Contact",
                column: "ClientId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_Contact_AgentId",
                table: "QuoteRequests",
                column: "AgentId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_OwnedById",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_AssistantId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_ClientId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Contact_TeamId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_Contact_AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_Contact_AssistantId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ClientId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Contact");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "QuoteRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Opportunities",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnedById",
                table: "Contact",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_TeamId",
                table: "QuoteRequests",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactId",
                table: "Contact",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Contact_ContactId",
                table: "Contact",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Teams_OwnedById",
                table: "Contact",
                column: "OwnedById",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Teams_TeamId",
                table: "Opportunities",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_Teams_TeamId",
                table: "QuoteRequests",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
