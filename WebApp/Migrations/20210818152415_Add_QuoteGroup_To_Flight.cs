using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Add_QuoteGroup_To_Flight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets");

            migrationBuilder.RenameColumn(
                name: "QuoteGroupId",
                table: "QuoteRequestTickets",
                newName: "QuoteGroupID");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_QuoteGroupId",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_QuoteGroupID");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteGroupID",
                table: "QuoteRequestTickets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupID",
                table: "QuoteRequestTickets",
                column: "QuoteGroupID",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupID",
                table: "QuoteRequestTickets");

            migrationBuilder.RenameColumn(
                name: "QuoteGroupID",
                table: "QuoteRequestTickets",
                newName: "QuoteGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestTickets_QuoteGroupID",
                table: "QuoteRequestTickets",
                newName: "IX_QuoteRequestTickets_QuoteGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteGroupId",
                table: "QuoteRequestTickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestTickets_QuoteGroups_QuoteGroupId",
                table: "QuoteRequestTickets",
                column: "QuoteGroupId",
                principalTable: "QuoteGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
