using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMemo",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetPayment",
                table: "Booking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "PaymentMemo",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "TargetPayment",
                table: "Booking");
        }
    }
}
