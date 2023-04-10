using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Passport_PassportID",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "Passport");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PassportID",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PassportID",
                table: "AppUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassportID",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Passport",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Issued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passport", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PassportID",
                table: "AppUsers",
                column: "PassportID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Passport_PassportID",
                table: "AppUsers",
                column: "PassportID",
                principalTable: "Passport",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
