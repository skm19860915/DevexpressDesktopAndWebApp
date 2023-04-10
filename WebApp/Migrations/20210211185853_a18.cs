using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class a18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HouseHolds",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "First",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Last",
                table: "Contact",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "First",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Last",
                table: "Contact");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "HouseHolds",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
