using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Confirmation",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FOPId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardType",
                table: "CreditCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepositDue",
                table: "Booking",
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
                name: "IX_Payments_FOPId",
                table: "Payments",
                column: "FOPId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_ContactId",
                table: "CreditCards",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Contact_ContactId",
                table: "CreditCards",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CreditCards_FOPId",
                table: "Payments",
                column: "FOPId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Contact_ContactId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CreditCards_FOPId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_FOPId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_ContactId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "Confirmation",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "FOPId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "DepositDue",
                table: "Booking");

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
