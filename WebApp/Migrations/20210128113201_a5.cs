using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Results",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetCompanyId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetContactId",
                table: "Tasks",
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
                name: "IX_Tasks_TargetCompanyId",
                table: "Tasks",
                column: "TargetCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TargetContactId",
                table: "Tasks",
                column: "TargetContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Companies_TargetCompanyId",
                table: "Tasks",
                column: "TargetCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Contact_TargetContactId",
                table: "Tasks",
                column: "TargetContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Companies_TargetCompanyId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Contact_TargetContactId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TargetCompanyId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TargetContactId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Results",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TargetCompanyId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TargetContactId",
                table: "Tasks");

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
