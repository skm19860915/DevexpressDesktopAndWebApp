using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "UserStories");

            migrationBuilder.AddColumn<string>(
                name: "AcceptanceCriteria",
                table: "UserStories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserStories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NewsFeeds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: true),
                    OpportunityID = table.Column<int>(nullable: true),
                    TaskID = table.Column<int>(nullable: true),
                    UserStoryID = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    NewsTypeId = table.Column<int>(nullable: false),
                    SourceId = table.Column<int>(nullable: false),
                    SourceType = table.Column<int>(nullable: false),
                    ActionDateTime = table.Column<DateTime>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    News = table.Column<string>(nullable: true),
                    TimeAgo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeeds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_Opportunities_OpportunityID",
                        column: x => x.OpportunityID,
                        principalTable: "Opportunities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_NewsFeeds_ParentID",
                        column: x => x.ParentID,
                        principalTable: "NewsFeeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_UserStories_UserStoryID",
                        column: x => x.UserStoryID,
                        principalTable: "UserStories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_SprintID",
                table: "UserStories",
                column: "SprintID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_CompanyID",
                table: "NewsFeeds",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_ContactId",
                table: "NewsFeeds",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_OpportunityID",
                table: "NewsFeeds",
                column: "OpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_ParentID",
                table: "NewsFeeds",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_TaskID",
                table: "NewsFeeds",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_UserStoryID",
                table: "NewsFeeds",
                column: "UserStoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStories_Sprints_SprintID",
                table: "UserStories",
                column: "SprintID",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStories_Sprints_SprintID",
                table: "UserStories");

            migrationBuilder.DropTable(
                name: "NewsFeeds");

            migrationBuilder.DropIndex(
                name: "IX_UserStories_SprintID",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "AcceptanceCriteria",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Contact");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "UserStories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "UserStories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "UserStories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "UserStories",
                type: "datetime2",
                nullable: true);
        }
    }
}
