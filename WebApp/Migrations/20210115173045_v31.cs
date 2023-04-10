using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Opportunities_Contact_AgentId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_Contact_AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_AgentId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_AgentId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Contact_AgentId",
                table: "Contact");

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

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Contact");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "QuoteRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Opportunities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryTeamId",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnedById",
                table: "Contact",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RemoteUpdate = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PrimaryId = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Contact_PrimaryId",
                        column: x => x.PrimaryId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: true),
                    DatesCalculated = table.Column<bool>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ClientOrgID = table.Column<int>(nullable: false),
                    IssuerID = table.Column<string>(nullable: true),
                    OwnerID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    Completed = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: true),
                    OpportunityID = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatorID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FixedField = table.Column<int>(nullable: true),
                    UpdatedById = table.Column<string>(nullable: true),
                    SaveMask = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    DataType = table.Column<int>(nullable: false),
                    PercentComplete = table.Column<double>(nullable: false),
                    OutlookID = table.Column<string>(nullable: true),
                    TempDatesCalculated = table.Column<bool>(nullable: true),
                    usingProjectsPriority = table.Column<bool>(nullable: false),
                    LastChangedID = table.Column<int>(nullable: false),
                    LastChanged = table.Column<int>(nullable: true),
                    CompletedDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    SeqOrder = table.Column<int>(nullable: true),
                    DayLocked = table.Column<bool>(nullable: false),
                    EmailID = table.Column<int>(nullable: true),
                    Private = table.Column<bool>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: true),
                    MeetingID = table.Column<int>(nullable: true),
                    UserStoryID = table.Column<int>(nullable: true),
                    AppointGUID = table.Column<string>(nullable: true),
                    GUID = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    CompletedNotificationSent = table.Column<bool>(nullable: false),
                    ActiveBaseLineDate = table.Column<DateTime>(nullable: true),
                    Sunday = table.Column<bool>(nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    isReoccuring = table.Column<bool>(nullable: false),
                    ReOccuringStart = table.Column<DateTime>(nullable: true),
                    ReOccuringEnd = table.Column<DateTime>(nullable: true),
                    AppointmentStartTime = table.Column<string>(nullable: true),
                    AppointmentEndTime = table.Column<string>(nullable: true),
                    PrimaryOwnerID = table.Column<int>(nullable: false),
                    isMeeting = table.Column<bool>(nullable: true),
                    MileStone = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tasks_Contact_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Contact_IssuerID",
                        column: x => x.IssuerID,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Opportunities_OpportunityID",
                        column: x => x.OpportunityID,
                        principalTable: "Opportunities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Teams_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Contact_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_TeamId",
                table: "QuoteRequests",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_TeamId",
                table: "Opportunities",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PrimaryTeamId",
                table: "Contact",
                column: "PrimaryTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactId",
                table: "Contact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_OwnedById",
                table: "Contact",
                column: "OwnedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatorID",
                table: "Tasks",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IssuerID",
                table: "Tasks",
                column: "IssuerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OpportunityID",
                table: "Tasks",
                column: "OpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerID",
                table: "Tasks",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UpdatedById",
                table: "Tasks",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PrimaryId",
                table: "Teams",
                column: "PrimaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Teams_PrimaryTeamId",
                table: "Contact",
                column: "PrimaryTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Teams_PrimaryTeamId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Contact_ContactId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Teams_OwnedById",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Teams_TeamId",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequests_Teams_TeamId",
                table: "QuoteRequests");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRequests_TeamId",
                table: "QuoteRequests");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_TeamId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Contact_PrimaryTeamId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactId",
                table: "Contact");

            //migrationBuilder.DropIndex(
            //    name: "IX_Contact_OwnedById",
            //    table: "Contact");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "QuoteRequests");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "PrimaryTeamId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "OwnedById",
                table: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "QuoteRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Opportunities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssistantId",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_AgentId",
                table: "QuoteRequests",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_AgentId",
                table: "Opportunities",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AgentId",
                table: "Contact",
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
                name: "FK_Opportunities_Contact_AgentId",
                table: "Opportunities",
                column: "AgentId",
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
    }
}
