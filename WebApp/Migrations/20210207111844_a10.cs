using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "UserStories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeploymentDate",
                table: "UserStories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "DeploymentDate",
                table: "UserStories");
        }
    }
}
