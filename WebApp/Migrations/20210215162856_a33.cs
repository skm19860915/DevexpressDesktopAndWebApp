using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Opportunities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Opportunities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Opportunities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Opportunities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_CreatedById",
                table: "Opportunities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_UpdatedById",
                table: "Opportunities",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Contact_CreatedById",
                table: "Opportunities",
                column: "CreatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Contact_UpdatedById",
                table: "Opportunities",
                column: "UpdatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Contact_CreatedById",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Contact_UpdatedById",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_CreatedById",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_UpdatedById",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Opportunities");
        }
    }
}
