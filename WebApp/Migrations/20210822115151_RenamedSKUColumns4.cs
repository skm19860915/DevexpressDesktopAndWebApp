using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class RenamedSKUColumns4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUs_Companies_AccommodationId",
                table: "SKUs");

            migrationBuilder.DropIndex(
                name: "IX_SKUs_AccommodationId",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "AccommodiationID",
                table: "SKUs");

            migrationBuilder.AddColumn<int>(
                name: "ProviderID",
                table: "SKUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_SKUs_ProviderID",
                table: "SKUs",
                column: "ProviderID");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUs_Companies_ProviderID",
                table: "SKUs");

            migrationBuilder.DropIndex(
                name: "IX_SKUs_ProviderID",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "ProviderID",
                table: "SKUs");

            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "SKUs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccommodiationID",
                table: "SKUs",
                type: "int",
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

            migrationBuilder.CreateIndex(
                name: "IX_SKUs_AccommodationId",
                table: "SKUs",
                column: "AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUs_Companies_AccommodationId",
                table: "SKUs",
                column: "AccommodationId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
