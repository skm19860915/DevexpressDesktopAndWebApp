using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentAirPortPreferences_AppUsers_AgentId",
                table: "AgentAirPortPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_AIDefaultFilters_AppUsers_AgentId",
                table: "AIDefaultFilters");

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
                name: "FK_AppUsers_Relationships_RelationshipID",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Companies_CompanyId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_ContactSubTypes_ContactSubTypeId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_ContactTypes_ContactTypeId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_CreditCards_FoodBasket",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_HouseHolds_HouseHoldId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AppUsers_SalesPersonId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactTrip_AppUsers_ContactId",
                table: "ContactTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_AppUsers_UserId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AppUsers_ContactID",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_AppUsers_ContactId",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AppUsers_WriterId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_AppUsers_AgentId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AppUsers_AuthorID",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AppUsers_PayeeID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_AppUsers_UserId",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AppUsers_ApproverID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AppUsers_UserID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferredAirPort_AppUsers_ContactId",
                table: "PreferredAirPort");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_AppUsers_AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_AppUsers_BookedById",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationshipMaps_AppUsers_PrimaryId",
                table: "RelationshipMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationshipMaps_AppUsers_TargetId",
                table: "RelationshipMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMaps_AppUsers_UserID",
                table: "UserMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_WebSrvLogins_AppUsers_AgentId",
                table: "WebSrvLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.RenameTable(
                name: "AppUsers",
                newName: "Contact");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_HouseHoldId",
                table: "Contact",
                newName: "IX_Contact_HouseHoldId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_FoodBasket",
                table: "Contact",
                newName: "IX_Contact_FoodBasket");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_ContactTypeId",
                table: "Contact",
                newName: "IX_Contact_ContactTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_ContactSubTypeId",
                table: "Contact",
                newName: "IX_Contact_ContactSubTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_CompanyId",
                table: "Contact",
                newName: "IX_Contact_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_RelationshipID",
                table: "Contact",
                newName: "IX_Contact_RelationshipID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_ClientId",
                table: "Contact",
                newName: "IX_Contact_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_AssistantId",
                table: "Contact",
                newName: "IX_Contact_AssistantId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_AgentId",
                table: "Contact",
                newName: "IX_Contact_AgentId");
            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentAirPortPreferences_Contact_AgentId",
                table: "AgentAirPortPreferences",
                column: "AgentId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AIDefaultFilters_Contact_AgentId",
                table: "AIDefaultFilters",
                column: "AgentId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Contact_SalesPersonId",
                table: "Companies",
                column: "SalesPersonId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Contact_AgentId",
                table: "Contact",
                column: "AgentId",
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
                name: "FK_Contact_Relationships_RelationshipID",
                table: "Contact",
                column: "RelationshipID",
                principalTable: "Relationships",
                principalColumn: "RelationshipID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Companies_CompanyId",
                table: "Contact",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ContactSubTypes_ContactSubTypeId",
                table: "Contact",
                column: "ContactSubTypeId",
                principalTable: "ContactSubTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ContactTypes_ContactTypeId",
                table: "Contact",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_CreditCards_FoodBasket",
                table: "Contact",
                column: "FoodBasket",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_HouseHolds_HouseHoldId",
                table: "Contact",
                column: "HouseHoldId",
                principalTable: "HouseHolds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTrip_Contact_ContactId",
                table: "ContactTrip",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Contact_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Contact_ContactID",
                table: "Invoice",
                column: "ContactID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_Contact_ContactId",
                table: "MemberShips",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Contact_WriterId",
                table: "Notes",
                column: "WriterId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Contact_AgentId",
                table: "Opportunities",
                column: "AgentId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Contact_AuthorID",
                table: "Pages",
                column: "AuthorID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Contact_PayeeID",
                table: "Payment",
                column: "PayeeID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Contact_UserId",
                table: "Phone",
                column: "UserId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Contact_ApproverID",
                table: "Posts",
                column: "ApproverID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Contact_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferredAirPort_Contact_ContactId",
                table: "PreferredAirPort",
                column: "ContactId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Contact_BookedById",
                table: "Quotes",
                column: "BookedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationshipMaps_Contact_PrimaryId",
                table: "RelationshipMaps",
                column: "PrimaryId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationshipMaps_Contact_TargetId",
                table: "RelationshipMaps",
                column: "TargetId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMaps_Contact_UserID",
                table: "UserMaps",
                column: "UserID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebSrvLogins_Contact_AgentId",
                table: "WebSrvLogins",
                column: "AgentId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentAirPortPreferences_Contact_AgentId",
                table: "AgentAirPortPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_AIDefaultFilters_Contact_AgentId",
                table: "AIDefaultFilters");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Contact_SalesPersonId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_AgentId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_AssistantId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_ClientId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Relationships_RelationshipID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Companies_CompanyId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ContactSubTypes_ContactSubTypeId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ContactTypes_ContactTypeId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_CreditCards_FoodBasket",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_HouseHolds_HouseHoldId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactTrip_Contact_ContactId",
                table: "ContactTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Contact_UserId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Contact_ContactID",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_Contact_ContactId",
                table: "MemberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Contact_WriterId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Contact_AgentId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Contact_AuthorID",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Contact_PayeeID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Contact_UserId",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Contact_ApproverID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Contact_UserID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferredAirPort_Contact_ContactId",
                table: "PreferredAirPort");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_Contact_AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Contact_BookedById",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationshipMaps_Contact_PrimaryId",
                table: "RelationshipMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_RelationshipMaps_Contact_TargetId",
                table: "RelationshipMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMaps_Contact_UserID",
                table: "UserMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_WebSrvLogins_Contact_AgentId",
                table: "WebSrvLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "AppUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_HouseHoldId",
                table: "AppUsers",
                newName: "IX_AppUsers_HouseHoldId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_FoodBasket",
                table: "AppUsers",
                newName: "IX_AppUsers_FoodBasket");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ContactTypeId",
                table: "AppUsers",
                newName: "IX_AppUsers_ContactTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ContactSubTypeId",
                table: "AppUsers",
                newName: "IX_AppUsers_ContactSubTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_CompanyId",
                table: "AppUsers",
                newName: "IX_AppUsers_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_RelationshipID",
                table: "AppUsers",
                newName: "IX_AppUsers_RelationshipID");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_ClientId",
                table: "AppUsers",
                newName: "IX_AppUsers_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_AssistantId",
                table: "AppUsers",
                newName: "IX_AppUsers_AssistantId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_AgentId",
                table: "AppUsers",
                newName: "IX_AppUsers_AgentId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "Id");

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
                name: "FK_AppUsers_AppUsers_AgentId",
                table: "AppUsers",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_AppUsers_Relationships_RelationshipID",
                table: "AppUsers",
                column: "RelationshipID",
                principalTable: "Relationships",
                principalColumn: "RelationshipID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Companies_CompanyId",
                table: "AppUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_CreditCards_FoodBasket",
                table: "AppUsers",
                column: "FoodBasket",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_HouseHolds_HouseHoldId",
                table: "AppUsers",
                column: "HouseHoldId",
                principalTable: "HouseHolds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AppUsers_SalesPersonId",
                table: "Companies",
                column: "SalesPersonId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTrip_AppUsers_ContactId",
                table: "ContactTrip",
                column: "ContactId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_AppUsers_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AppUsers_ContactID",
                table: "Invoice",
                column: "ContactID",
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
                name: "FK_Notes_AppUsers_WriterId",
                table: "Notes",
                column: "WriterId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_AppUsers_AgentId",
                table: "Opportunities",
                column: "AgentId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AppUsers_AuthorID",
                table: "Pages",
                column: "AuthorID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AppUsers_PayeeID",
                table: "Payment",
                column: "PayeeID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_AppUsers_UserId",
                table: "Phone",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AppUsers_ApproverID",
                table: "Posts",
                column: "ApproverID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AppUsers_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferredAirPort_AppUsers_ContactId",
                table: "PreferredAirPort",
                column: "ContactId",
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
                name: "FK_Quotes_AppUsers_BookedById",
                table: "Quotes",
                column: "BookedById",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationshipMaps_AppUsers_PrimaryId",
                table: "RelationshipMaps",
                column: "PrimaryId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationshipMaps_AppUsers_TargetId",
                table: "RelationshipMaps",
                column: "TargetId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMaps_AppUsers_UserID",
                table: "UserMaps",
                column: "UserID",
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
    }
}
