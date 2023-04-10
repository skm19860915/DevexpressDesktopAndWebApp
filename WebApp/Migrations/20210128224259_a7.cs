using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserStoryId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprints_Contact_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserStories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<string>(nullable: true),
                    IssuerID = table.Column<string>(nullable: true),
                    OwnerID = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Private = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    SprintID = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    PercentComplete = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStories_Contact_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStories_Contact_IssuerID",
                        column: x => x.IssuerID,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStories_Contact_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStories_Contact_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserStoryId",
                table: "Tasks",
                column: "UserStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_OwnerID",
                table: "Sprints",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_CreatedById",
                table: "UserStories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_IssuerID",
                table: "UserStories",
                column: "IssuerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_LastUpdatedById",
                table: "UserStories",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_OwnerID",
                table: "UserStories",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_UserStories_UserStoryId",
                table: "Tasks",
                column: "UserStoryId",
                principalTable: "UserStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_UserStories_UserStoryId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "UserStories");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserStoryId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserStoryId",
                table: "Tasks");
        }
    }
}
