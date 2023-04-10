using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Operational",
                table: "UserStories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Operational",
                table: "Features",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemId",
                table: "Features",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Operational = table.Column<int>(nullable: false),
                    OwnerID = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedById = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Systems_Contact_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Systems_Contact_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Systems_Contact_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_SystemId",
                table: "Features",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_CreatedById",
                table: "Systems",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_LastUpdatedById",
                table: "Systems",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_OwnerID",
                table: "Systems",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Systems_SystemId",
                table: "Features",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Systems_SystemId",
                table: "Features");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropIndex(
                name: "IX_Features_SystemId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Operational",
                table: "UserStories");

            migrationBuilder.DropColumn(
                name: "Operational",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Features");
        }
    }
}
