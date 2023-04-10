using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentAirPortPreferences_AppUsers_AgentID",
                table: "AgentAirPortPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_AIDefaultFilters_AppUsers_AgentID",
                table: "AIDefaultFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactSubTypes_ContactTypes_AgentID",
                table: "ContactSubTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AppUsers_AgentID",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_AppUsers_AgentID",
                table: "QuoteRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_WebSrvLogins_AppUsers_AgentID",
                table: "WebSrvLogins");

            migrationBuilder.DropIndex(
                name: "IX_ContactSubTypes_AgentID",
                table: "ContactSubTypes");

            migrationBuilder.DropColumn(
                name: "AgentID",
                table: "ContactSubTypes");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "WebSrvLogins",
                newName: "AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_WebSrvLogins_AgentID",
                table: "WebSrvLogins",
                newName: "IX_WebSrvLogins_AgentId");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "QuoteRequests",
                newName: "AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequests_AgentID",
                table: "QuoteRequests",
                newName: "IX_QuoteRequests_AgentId");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "Opportunities",
                newName: "AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_Opportunities_AgentID",
                table: "Opportunities",
                newName: "IX_Opportunities_AgentId");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "AIDefaultFilters",
                newName: "AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_AIDefaultFilters_AgentID",
                table: "AIDefaultFilters",
                newName: "IX_AIDefaultFilters_AgentId");

            migrationBuilder.RenameColumn(
                name: "AgentID",
                table: "AgentAirPortPreferences",
                newName: "AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_AgentAirPortPreferences_AgentID",
                table: "AgentAirPortPreferences",
                newName: "IX_AgentAirPortPreferences_AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactSubTypes_ContactTypeId",
                table: "ContactSubTypes",
                column: "ContactTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentAirPortPreferences_AppUsers_AgentId",
                table: "AgentAirPortPreferences",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AIDefaultFilters_AppUsers_AgentId",
                table: "AIDefaultFilters",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactSubTypes_ContactTypes_ContactTypeId",
                table: "ContactSubTypes",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AppUsers_AgentId",
                table: "Opportunities",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_AppUsers_AgentId",
                table: "QuoteRequests",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebSrvLogins_AppUsers_AgentId",
                table: "WebSrvLogins",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentAirPortPreferences_AppUsers_AgentId",
                table: "AgentAirPortPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_AIDefaultFilters_AppUsers_AgentId",
                table: "AIDefaultFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactSubTypes_ContactTypes_ContactTypeId",
                table: "ContactSubTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AppUsers_AgentId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_AppUsers_AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_WebSrvLogins_AppUsers_AgentId",
                table: "WebSrvLogins");

            migrationBuilder.DropIndex(
                name: "IX_ContactSubTypes_ContactTypeId",
                table: "ContactSubTypes");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "WebSrvLogins",
                newName: "AgentID");

            migrationBuilder.RenameIndex(
                name: "IX_WebSrvLogins_AgentId",
                table: "WebSrvLogins",
                newName: "IX_WebSrvLogins_AgentID");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "QuoteRequests",
                newName: "AgentID");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequests_AgentId",
                table: "QuoteRequests",
                newName: "IX_QuoteRequests_AgentID");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "Opportunities",
                newName: "AgentID");

            migrationBuilder.RenameIndex(
                name: "IX_Opportunities_AgentId",
                table: "Opportunities",
                newName: "IX_Opportunities_AgentID");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "AIDefaultFilters",
                newName: "AgentID");

            migrationBuilder.RenameIndex(
                name: "IX_AIDefaultFilters_AgentId",
                table: "AIDefaultFilters",
                newName: "IX_AIDefaultFilters_AgentID");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "AgentAirPortPreferences",
                newName: "AgentID");

            migrationBuilder.RenameIndex(
                name: "IX_AgentAirPortPreferences_AgentId",
                table: "AgentAirPortPreferences",
                newName: "IX_AgentAirPortPreferences_AgentID");

            migrationBuilder.AddColumn<int>(
                name: "AgentID",
                table: "ContactSubTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactSubTypes_AgentID",
                table: "ContactSubTypes",
                column: "AgentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentAirPortPreferences_AppUsers_AgentID",
                table: "AgentAirPortPreferences",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AIDefaultFilters_AppUsers_AgentID",
                table: "AIDefaultFilters",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactSubTypes_ContactTypes_AgentID",
                table: "ContactSubTypes",
                column: "AgentID",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AppUsers_AgentID",
                table: "Opportunities",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequests_AppUsers_AgentID",
                table: "QuoteRequests",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebSrvLogins_AppUsers_AgentID",
                table: "WebSrvLogins",
                column: "AgentID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
