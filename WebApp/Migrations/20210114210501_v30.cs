using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Booking_BookingID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Contact_PayeeID",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_PayeeID",
                table: "Payments",
                newName: "IX_Payments_PayeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_BookingID",
                table: "Payments",
                newName: "IX_Payments_BookingID");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Payments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Memo",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Payments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreatedById",
                table: "Payments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UpdatedById",
                table: "Payments",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Booking_BookingID",
                table: "Payments",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contact_CreatedById",
                table: "Payments",
                column: "CreatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contact_PayeeID",
                table: "Payments",
                column: "PayeeID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contact_UpdatedById",
                table: "Payments",
                column: "UpdatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Booking_BookingID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contact_CreatedById",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contact_PayeeID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contact_UpdatedById",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CreatedById",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UpdatedById",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Memo",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PayeeID",
                table: "Payment",
                newName: "IX_Payment_PayeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BookingID",
                table: "Payment",
                newName: "IX_Payment_BookingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "PaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Booking_BookingID",
                table: "Payment",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Contact_PayeeID",
                table: "Payment",
                column: "PayeeID",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
