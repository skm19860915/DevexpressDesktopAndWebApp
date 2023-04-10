using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v61 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_BusinessType_BusinessTypeID",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessType",
                table: "BusinessType");

            migrationBuilder.RenameTable(
                name: "BusinessType",
                newName: "BusinessTypes");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessTypes",
                table: "BusinessTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_BusinessTypes_BusinessTypeID",
                table: "Companies",
                column: "BusinessTypeID",
                principalTable: "BusinessTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_BusinessTypes_BusinessTypeID",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessTypes",
                table: "BusinessTypes");

            migrationBuilder.RenameTable(
                name: "BusinessTypes",
                newName: "BusinessType");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessType",
                table: "BusinessType",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_BusinessType_BusinessTypeID",
                table: "Companies",
                column: "BusinessTypeID",
                principalTable: "BusinessType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
