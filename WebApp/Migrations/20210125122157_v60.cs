using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class v60 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_BusinessType_BusinessTypeID",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessTypeID",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_BusinessType_BusinessTypeID",
                table: "Companies",
                column: "BusinessTypeID",
                principalTable: "BusinessType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_BusinessType_BusinessTypeID",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessTypeID",
                table: "Companies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_BusinessType_BusinessTypeID",
                table: "Companies",
                column: "BusinessTypeID",
                principalTable: "BusinessType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
