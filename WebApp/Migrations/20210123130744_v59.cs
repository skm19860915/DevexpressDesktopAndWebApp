using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v59 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Contact_SalesPersonId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_Companies_CompanyId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Opportunities_CompanyId",
                table: "Opportunities");

            migrationBuilder.DropIndex(
                name: "IX_Companies_SalesPersonId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visiblity",
                table: "Contact",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Contact_EmployerId",
                table: "Contact",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Companies_EmployerId",
                table: "Contact",
                column: "EmployerId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Companies_EmployerId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_EmployerId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Visiblity",
                table: "Contact");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Opportunities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesPersonId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_CompanyId",
                table: "Opportunities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SalesPersonId",
                table: "Companies",
                column: "SalesPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Contact_SalesPersonId",
                table: "Companies",
                column: "SalesPersonId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_Companies_CompanyId",
                table: "Opportunities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
