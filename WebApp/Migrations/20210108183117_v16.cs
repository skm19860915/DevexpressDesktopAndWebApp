using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GUID",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Locked",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "Quotes");

            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "QuoteGroups",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "QuoteGroups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "QuoteGroups",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Viewed",
                table: "QuoteGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GUID",
                table: "QuoteGroups");

            migrationBuilder.DropColumn(
                name: "Locked",
                table: "QuoteGroups");

            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "QuoteGroups");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "QuoteGroups");

            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "Quotes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "Quotes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Viewed",
                table: "Quotes",
                type: "datetime2",
                nullable: true);
        }
    }
}
