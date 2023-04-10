using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Booking");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Booking",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreatedById",
                table: "Booking",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UpdatedById",
                table: "Booking",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Contact_CreatedById",
                table: "Booking",
                column: "CreatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Contact_UpdatedById",
                table: "Booking",
                column: "UpdatedById",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_CreatedById",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_UpdatedById",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CreatedById",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_UpdatedById",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Booking");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }
    }
}
