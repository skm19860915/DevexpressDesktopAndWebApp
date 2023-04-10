﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddTicketToQuoteGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuoteRequestTicketId",
                table: "QuoteGroups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedQuoteRequestTicketId",
                table: "QuoteGroups",
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

            migrationBuilder.CreateIndex(
                name: "IX_QuoteGroups_QuoteRequestTicketId",
                table: "QuoteGroups",
                column: "QuoteRequestTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups",
                column: "QuoteRequestTicketId",
                principalTable: "QuoteRequestTickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteGroups_QuoteRequestTickets_QuoteRequestTicketId",
                table: "QuoteGroups");

            migrationBuilder.DropIndex(
                name: "IX_QuoteGroups_QuoteRequestTicketId",
                table: "QuoteGroups");

            migrationBuilder.DropColumn(
                name: "QuoteRequestTicketId",
                table: "QuoteGroups");

            migrationBuilder.DropColumn(
                name: "SelectedQuoteRequestTicketId",
                table: "QuoteGroups");

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
        }
    }
}
