using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_CreditCards_FoodBasket",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Contact_ContactId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_ContactId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Contact_FoodBasket",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "FoodBasket",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "FoodBasketId",
                table: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CreditCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "CreditCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CreatedById",
                table: "CreditCards",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_OwnerID",
                table: "CreditCards",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UpdatedById",
                table: "CreditCards",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Contact_CreatedById",
                table: "CreditCards",
                column: "CreatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Contact_OwnerID",
                table: "CreditCards",
                column: "OwnerID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Contact_UpdatedById",
                table: "CreditCards",
                column: "UpdatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Contact_CreatedById",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Contact_OwnerID",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Contact_UpdatedById",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_CreatedById",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_OwnerID",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_UpdatedById",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "CreditCards");

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "CreditCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FoodBasket",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FoodBasketId",
                table: "Contact",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_ContactId",
                table: "CreditCards",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_FoodBasket",
                table: "Contact",
                column: "FoodBasket");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_CreditCards_FoodBasket",
                table: "Contact",
                column: "FoodBasket",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Contact_ContactId",
                table: "CreditCards",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
